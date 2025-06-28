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
        private MMDeviceEnumerator _deviceEnumerator;

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

        [MemberNotNull(["PinnedOutputDevices", "PinnedInputDevices", "PinnedOutputCommDevices", "PinnedInputCommDevices"])]
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
            // Spin this off async to prevent UI blocking
            Task.Run(() =>
            {
                OnDefaultDeviceChanged(EDataFlow.eRender, ERole.eConsole, string.Empty);
                OnDefaultDeviceChanged(EDataFlow.eCapture, ERole.eConsole, string.Empty);
                if (Properties.Settings.Default.CommOut)
                {
                    OnDefaultDeviceChanged(EDataFlow.eRender, ERole.eCommunications, string.Empty);
                }
                if (Properties.Settings.Default.CommIn)
                {
                    OnDefaultDeviceChanged(EDataFlow.eCapture, ERole.eCommunications, string.Empty);
                }
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
            var connectedPins = GetActivePinnedDevices(devices, flow, role);

            if (connectedPins.Count > 0 && connectedPins.First().ID != defaultDeviceId)
            {
                SetDefaultDevice(connectedPins.First().ID, role);
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

            switch (role) {
                case ERole.eConsole:
                case ERole.eMultimedia:
                    return flow == EDataFlow.eRender 
                        ? [..PinnedOutputDevices.Where(devIdFilter)]
                        : [..PinnedInputDevices.Where(devIdFilter)];
                case ERole.eCommunications:
                    return flow == EDataFlow.eRender 
                        ? [..(syncComms ? PinnedOutputCommDevices : PinnedOutputDevices).Where(devIdFilter)]
                        : [..(syncComms ? PinnedInputCommDevices : PinnedInputDevices).Where(devIdFilter)];
                default:
                    return [];
            }
        }

        #region Ignored Callbacks

        public void OnDeviceStateChanged([MarshalAs(UnmanagedType.LPWStr)] string deviceId, [MarshalAs(UnmanagedType.I4)] EDeviceState newState)
        {
            // Do nothing
        }

        public void OnDeviceAdded([MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId)
        {
            // Do nothing
        }

        public void OnDeviceRemoved([MarshalAs(UnmanagedType.LPWStr)] string deviceId)
        {
            // Do nothing
        }

        public void OnPropertyValueChanged([MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId, PropertyKey key)
        {
            // Do nothing
        }

        #endregion
    }
}
