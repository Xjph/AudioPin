namespace AudioPin
{
    internal class TrayMenu : ContextMenuStrip
    {
        private readonly MainForm _mainForm;
        private readonly AudioDeviceManager _audioDeviceManager;
        private readonly ToolStripMenuItem _outputMenu;
        private readonly ToolStripMenuItem _inputMenu;

        public TrayMenu(MainForm mainForm, AudioDeviceManager audioDeviceManager)
        {
            _mainForm = mainForm;
            _audioDeviceManager = audioDeviceManager;

            _outputMenu = new("Output");
            _outputMenu.DropDownItems.Add("Placeholder");

            _inputMenu = new("Input");
            _inputMenu.DropDownItems.Add("Placeholder");

            Items.Add(_outputMenu);
            Items.Add(_inputMenu);

            Items.Add("-"); // Separator

            Items.Add("Open", null, (s, args) =>
            {
                mainForm.Show();
                mainForm.WindowState = FormWindowState.Normal;
            });
            
            Items.Add("Exit", null, (s, args) =>
            {
                Application.Exit();
            });
        }

        public void RefreshMenus()
        {
            Task.Run(() =>
            {
                RefreshMenu(_outputMenu,
                    _audioDeviceManager.GetOutputDevices,
                    _audioDeviceManager.PinnedOutputDevices,
                    _audioDeviceManager.CurrentDefaultOutput,
                    _mainForm.OutputList);
                RefreshMenu(_inputMenu,
                    _audioDeviceManager.GetInputDevices,
                    _audioDeviceManager.PinnedInputDevices,
                    _audioDeviceManager.CurrentDefaultInput,
                    _mainForm.InputList);
            });
        }

        private void RefreshMenu(ToolStripMenuItem menu, 
            List<AudioDevice> connectedDevices,
            List<AudioDevice> pinnedDevices,
            AudioDevice currentDevice,
            ListBox deviceList)
        {
            _mainForm.Invoke(()=>menu.DropDownItems.Clear());

            foreach (var device in connectedDevices)
            {
                var item = new ToolStripMenuItem(device.Name, null, (s, args) =>
                {
                    if (device.ID == currentDevice?.ID) return;

                    var pinnedMatch = pinnedDevices.Where(d => d.Name == device.Name);

                    if (pinnedMatch.Any())
                    {
                        var index = pinnedDevices.IndexOf(pinnedMatch.First());
                        MainForm.Swap(pinnedDevices, 0, index);
                        MainForm.Swap(deviceList, 0, index);
                    }
                    else
                    {
                        pinnedDevices.Insert(0, device);
                        deviceList.Items.Insert(0, device.Name);
                    }
                    _audioDeviceManager.SavePinnedDevices();

                })
                {
                    Checked = device.ID == currentDevice?.ID
                };
                _mainForm.Invoke(()=> menu.DropDownItems.Add(item));
            }
        }
    }
}
