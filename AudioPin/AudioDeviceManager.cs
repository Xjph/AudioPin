using CoreAudioApi.Interfaces;
using CoreAudioApi;
using System.Data;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace AudioPin
{
    public class AudioDeviceManager : IMMNotificationClient
    {
        private readonly MMDeviceEnumerator _deviceEnumerator;
        private AudioDevice? _currentDefaultOutput;
        private AudioDevice? _currentDefaultInput;

        public AudioDeviceManager()
        {
            _deviceEnumerator = new MMDeviceEnumerator();
            _deviceEnumerator.RegisterEndpointNotificationCallback(this);
            RetrievePinnedDevices();
        }

        public List<AudioDevice> PinnedOutputDevices;
        public List<AudioDevice> PinnedInputDevices;

        public List<AudioDevice> PinnedOutputCommDevices;
        public List<AudioDevice> PinnedInputCommDevices;

        public List<AudioDevice> GetOutputDevices => GetDevices(EDataFlow.eRender);

        public List<AudioDevice> GetInputDevices => GetDevices(EDataFlow.eCapture);

        public AudioDevice CurrentDefaultOutput 
        {
            get => _currentDefaultOutput ?? new();
        }

        public AudioDevice CurrentDefaultInput 
        {
            get => _currentDefaultInput ?? new();
        }

        [MemberNotNull([
            "PinnedOutputDevices", 
            "PinnedInputDevices", 
            "PinnedOutputCommDevices", 
            "PinnedInputCommDevices"])]
        private void RetrievePinnedDevices()
        {
            PinnedOutputDevices = [
                .. Psv2List(Properties.Settings.Default.PinnedOut)
                .Select(id => GetDeviceById(id, EDataFlow.eRender))
                ];
            PinnedInputDevices = [
                .. Psv2List(Properties.Settings.Default.PinnedIn)
                .Select(id => GetDeviceById(id, EDataFlow.eCapture))
                ];
            PinnedOutputCommDevices = [
                .. Psv2List(Properties.Settings.Default.PinnedOutComm)
                .Select(id => GetDeviceById(id, EDataFlow.eRender))
                ];
            PinnedInputCommDevices = [
                .. Psv2List(Properties.Settings.Default.PinnedInComm)
                .Select(id => GetDeviceById(id, EDataFlow.eCapture))
                ];

            AssertPins();
        }

        private void AssertPins()
        {
            HealDeviceIds();
            // Spin this off async to prevent UI blocking
            Task.Run(() =>
            {
                OnDefaultDeviceChanged(EDataFlow.eRender, ERole.eConsole, string.Empty);
                OnDefaultDeviceChanged(EDataFlow.eCapture, ERole.eConsole, string.Empty);
                OnDefaultDeviceChanged(EDataFlow.eRender, ERole.eCommunications, string.Empty);
                OnDefaultDeviceChanged(EDataFlow.eCapture, ERole.eCommunications, string.Empty);
            });
        }

        public void SavePinnedDevices()
        {
            Properties.Settings.Default.PinnedOut = List2Psv(PinnedOutputDevices.Select(d => d.ID));
            Properties.Settings.Default.PinnedIn = List2Psv(PinnedInputDevices.Select(d => d.ID));
            Properties.Settings.Default.PinnedOutComm = List2Psv(PinnedOutputCommDevices.Select(d => d.ID));
            Properties.Settings.Default.PinnedInComm = List2Psv(PinnedInputCommDevices.Select(d => d.ID));
            Properties.Settings.Default.Save();
            AssertPins();
        }

        private AudioDevice GetDeviceById(string id, EDataFlow flow)
        {
            Dictionary<string, string> seenDevices;
            try
            {
                seenDevices = JsonSerializer.Deserialize<Dictionary<string, string>>(Properties.Settings.Default.KnownDevices) ?? [];
            }
            catch
            {
                seenDevices = [];
            }

            if (TryGetDevice(id, out AudioDevice device))
            {
                if (!seenDevices.ContainsKey(id))
                {
                    seenDevices.Add(id, device.Name);
                    Properties.Settings.Default.KnownDevices = JsonSerializer.Serialize(seenDevices);
                    Properties.Settings.Default.Save();
                }
                return device;
            }
            else
            {
                if (seenDevices.TryGetValue(id, out string? value))
                {
                    if (TryGetDeviceByName(value, flow, out AudioDevice namedDevice))
                    {
                        return namedDevice;
                    }
                    else
                    {
                        return new AudioDevice
                        {
                            ID = id,
                            Name = value
                        };
                    }
                }
                else
                {
                    return new AudioDevice
                    {
                        ID = id,
                        Name = "Unknown Device"
                    };
                }
            }
        }

        private bool TryGetDevice(string id, out AudioDevice outDevice)
        {
            try
            {
                var device = _deviceEnumerator.GetDevice(id);
                if (device != null)
                {
                    outDevice = new()
                    {
                        ID = device.ID,
                        Name = device.FriendlyName
                    };
                    return true;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                outDevice = new();
                return false;
            }
        }

        private bool TryGetDeviceByName(string name, EDataFlow flow, out AudioDevice outDevice)
        {
            var devices = _deviceEnumerator.EnumerateAudioEndPoints(flow, EDeviceState.DEVICE_STATE_ACTIVE);
            var namedDevice = devices.Where(d => d.FriendlyName == name);
            if (namedDevice.Any())
            {
                outDevice = new AudioDevice
                {
                    ID = namedDevice.First().ID,
                    Name = namedDevice.First().FriendlyName
                };
                return true;
            }
            else
            {
                outDevice = new AudioDevice();
                return false;
            }
        }


        private static List<string> Psv2List(string csv)
        {
            return [.. csv.Split('|', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim())];
        }

        private static string List2Psv(IEnumerable<string> list)
        {
            return string.Join('|', list);
        }

        public List<AudioDevice> GetDevices(EDataFlow eDataFlow)
        {
            var devices = _deviceEnumerator.EnumerateAudioEndPoints(eDataFlow, EDeviceState.DEVICE_STATE_ACTIVE);

            return [.. devices.Select(
                device => new AudioDevice() 
                { 
                    Name = device.FriendlyName, 
                    ID = device.ID 
                })];

        }

        public static bool SetDefaultDevice(string ID, ERole role)
        {
            try
            {
                var client = new PolicyConfigClient();
                client.SetDefaultEndpoint(ID, role);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void OnDefaultDeviceChanged(EDataFlow flow, ERole role, [MarshalAs(UnmanagedType.LPWStr)] string defaultDeviceId)
        {
            var devices = GetDevices(flow);
            ERole pinRole = 
                (flow == EDataFlow.eCapture && !Properties.Settings.Default.CommIn
                || flow == EDataFlow.eRender && !Properties.Settings.Default.CommOut)
                ? ERole.eConsole
                : role;
            
            var connectedPins = GetActivePinnedDevices(devices, flow, pinRole);

            if (connectedPins.Count > 0 && connectedPins.First().ID != defaultDeviceId)
            {
                SetDefaultDevice(connectedPins.First().ID, role);
                if (flow == EDataFlow.eRender && role != ERole.eCommunications)
                {
                    _currentDefaultOutput = connectedPins.First();
                }
                else
                {
                    _currentDefaultInput = connectedPins.First();
                }
            }
        }

        private List<AudioDevice> GetActivePinnedDevices(IEnumerable<AudioDevice> devices, EDataFlow flow, ERole role)
        {
            // Should match on name first, that's what users see and ID is not always stable
            bool devIdFilter(AudioDevice d) => devices.Any(dev => 
                dev.Name == d.Name 
                || dev.ID == d.ID);

            bool syncComms = flow == EDataFlow.eRender
                ? Properties.Settings.Default.CommOut
                : Properties.Settings.Default.CommIn;

            var activeDevices = role switch
            {
                var f when (f == ERole.eConsole || f == ERole.eMultimedia) => flow == EDataFlow.eRender ? PinnedOutputDevices.Where(devIdFilter) : PinnedInputDevices.Where(devIdFilter),
                ERole.eCommunications => flow == EDataFlow.eRender 
                    ? (syncComms ? PinnedOutputCommDevices : PinnedOutputDevices).Where(devIdFilter) 
                    : (syncComms ? PinnedInputCommDevices : PinnedInputDevices).Where(devIdFilter),
                _ => []
            };

            // Update IDs here if mismatched.
            bool save = false;
            foreach (var device in activeDevices)
            {
                var match = devices.FirstOrDefault(d => d.Name == device.Name);
                if (match != null && match.ID != device.ID)
                {
                    device.ID = match.ID;
                    save = true;
                }
            }

            if (save) SavePinnedDevices();

            return [..activeDevices];
        }

        // Device IDs are unstable and can change when devices are added/removed and reenumerated.
        private bool HealDeviceIds()
        {
            bool changed = false;
            var inputDevices = GetDevices(EDataFlow.eCapture);
            var outputDevices = GetDevices(EDataFlow.eRender);

            foreach (var device in PinnedInputDevices)
            {
                if (!inputDevices.Any(d => d.ID == device.ID))
                {
                    var match = inputDevices.FirstOrDefault(d => d.Name == device.Name);
                    if (match != null && match.ID != device.ID)
                    {
                        device.ID = match.ID;
                        changed = true;
                    }
                }
            }

            foreach (var device in PinnedOutputDevices)
            {
                if (!outputDevices.Any(d => d.ID == device.ID))
                {
                    var match = outputDevices.FirstOrDefault(d => d.Name == device.Name);
                    if (match != null && match.ID != device.ID)
                    {
                        device.ID = match.ID;
                        changed = true;
                    }
                }
            }

            if (changed) SavePinnedDevices();
            return changed;
        }

        public void OnDeviceStateChanged([MarshalAs(UnmanagedType.LPWStr)] string deviceId, [MarshalAs(UnmanagedType.I4)] EDeviceState newState)
        {
            HealDeviceIds();
        }

        public void OnDeviceAdded([MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId)
        {
            HealDeviceIds();
        }

        public void OnDeviceRemoved([MarshalAs(UnmanagedType.LPWStr)] string deviceId)
        {
            HealDeviceIds();
        }

        public void OnPropertyValueChanged([MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId, PropertyKey key)
        {
            // Do nothing
        }

        
    }
}
