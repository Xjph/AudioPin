using CoreAudioApi;

namespace AudioPin
{
    public partial class DeviceSelect : Form
    {
        private Dictionary<int, AudioDevice> _enumeratedDevices = [];
        private int _selectedIndex = -1;
        private readonly AudioDeviceManager _audioDeviceManager;

        public DeviceSelect(AudioDeviceManager audioDeviceManager, EDataFlow dataFlow)
        {
            InitializeComponent();

            _audioDeviceManager = audioDeviceManager;

            foreach (var device in _audioDeviceManager.GetDevices(dataFlow))
            {
                DeviceDropDown.Items.Add(device.Name);
                _enumeratedDevices.Add(DeviceDropDown.Items.Count - 1, device);
            }
            
            CommCheck.Visible =
                (dataFlow == EDataFlow.eRender && Properties.Settings.Default.CommOut) ||
                (dataFlow == EDataFlow.eCapture && Properties.Settings.Default.CommIn);
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            DialogResult = _selectedIndex == -1 ? DialogResult.Cancel : DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            _selectedIndex = -1; // Reset selected index on cancel
            Close();
        }

        private void DeviceDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedIndex = DeviceDropDown.SelectedIndex;
        }

        public string SelectedDeviceID
        {
            get
            {
                if (_selectedIndex >= 0 && _enumeratedDevices.ContainsKey(_selectedIndex))
                {
                    return _enumeratedDevices[_selectedIndex].ID;
                }
                return string.Empty; // Return empty if no valid selection
            }
        }

        public string SelectedDeviceName
        {
            get
            {
                if (_selectedIndex >= 0 && _enumeratedDevices.ContainsKey(_selectedIndex))
                {
                    return _enumeratedDevices[_selectedIndex].Name;
                }
                return string.Empty; // Return empty if no valid selection
            }
        }

        public bool CommDevice
        {
            get => CommCheck.Checked;
        }
    }
}
