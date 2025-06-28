using CoreAudioApi;
using Microsoft.Win32;
using System.Diagnostics;

namespace AudioPin
{
    public partial class MainForm : Form
    {
        #region Fields
        private AudioDeviceManager _audioDeviceManager;
        private HttpClient _httpClient = new();
        public bool HideTray = false;
        #endregion

        #region External List Accessors
        public ListBox OutputList => OutList;
        public ListBox InputList => InList;
        #endregion

        #region Constructor
        public MainForm(AudioDeviceManager audioDeviceManager)
        {
            _audioDeviceManager = audioDeviceManager;
            InitializeComponent();
            Icon = Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location);
            TrayIcon.Icon = Icon;
            var trayMenu = new TrayMenu(this, audioDeviceManager);
            TrayIcon.ContextMenuStrip = trayMenu;
            TrayIcon.Click += (_,_) => trayMenu.RefreshMenus();
            LoadSettings();
            var version = System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version??new Version();
            Text += $" - v{version.Major}.{version.Minor}.{version.Build}";
            CheckUpdate();
            if (AutoLaunchEnabled())
            {
                AutoLaunchCheckbox.Checked = true;
            }
            else
            {
                AutoLaunchCheckbox.Checked = false;
            }
            AutoLaunchCheckbox.CheckedChanged += AutoLaunchCheckbox_CheckedChanged;
        }
        #endregion

        #region Settings
        private void LoadSettings()
        {
            OutCommCheck.Checked = Properties.Settings.Default.CommOut;
            InCommCheck.Checked = Properties.Settings.Default.CommIn;
            OutCommCheck_CheckedChanged(this, new());
            InCommCheck_CheckedChanged(this, new());
            _audioDeviceManager.PinnedOutputDevices.ForEach(device => OutList.Items.Add(device.Name));
            _audioDeviceManager.PinnedInputDevices.ForEach(device => InList.Items.Add(device.Name));
            _audioDeviceManager.PinnedOutputCommDevices.ForEach(device => OutCommList.Items.Add(device.Name));
            _audioDeviceManager.PinnedInputCommDevices.ForEach(device => InCommList.Items.Add(device.Name));
        }
        #endregion

        #region Device Management
        private void OutPlusButton_Click(object sender, EventArgs e)
        {
            var selectForm = new DeviceSelect(_audioDeviceManager, EDataFlow.eRender);
            selectForm.ShowDialog();
            if (selectForm.DialogResult == DialogResult.OK)
            {
                if (selectForm.CommDevice)
                {
                    OutCommList.Items.Add(selectForm.SelectedDeviceName);
                    _audioDeviceManager.PinnedOutputCommDevices.Add(new AudioDevice
                    {
                        ID = selectForm.SelectedDeviceID,
                        Name = selectForm.SelectedDeviceName
                    });
                }
                else
                {
                    OutList.Items.Add(selectForm.SelectedDeviceName);
                    _audioDeviceManager.PinnedOutputDevices.Add(new AudioDevice
                    {
                        ID = selectForm.SelectedDeviceID,
                        Name = selectForm.SelectedDeviceName
                    });
                }
                _audioDeviceManager.SavePinnedDevices();
            }
        }

        private void InPlusButton_Click(object sender, EventArgs e)
        {
            var selectForm = new DeviceSelect(_audioDeviceManager, EDataFlow.eCapture);
            selectForm.ShowDialog();
            if (selectForm.DialogResult == DialogResult.OK)
            {
                if (selectForm.CommDevice)
                {
                    InCommList.Items.Add(selectForm.SelectedDeviceName);
                    _audioDeviceManager.PinnedInputCommDevices.Add(new AudioDevice
                    {
                        ID = selectForm.SelectedDeviceID,
                        Name = selectForm.SelectedDeviceName
                    });
                }
                else
                {
                    InList.Items.Add(selectForm.SelectedDeviceName);
                    _audioDeviceManager.PinnedInputDevices.Add(new AudioDevice
                    {
                        ID = selectForm.SelectedDeviceID,
                        Name = selectForm.SelectedDeviceName
                    });
                }
                _audioDeviceManager.SavePinnedDevices();
            }
        }

        private void OutMinusButton_Click(object sender, EventArgs e)
        {
            var removedIndex = OutList.SelectedIndex;
            if (removedIndex > -1)
            {
                OutList.Items.RemoveAt(removedIndex);
                _audioDeviceManager.PinnedOutputDevices.RemoveAt(removedIndex);
            }
            else
            {
                removedIndex = OutCommList.SelectedIndex;
                if (removedIndex > -1)
                {
                    OutCommList.Items.RemoveAt(removedIndex);
                    _audioDeviceManager.PinnedOutputCommDevices.RemoveAt(removedIndex);
                }
            }
            _audioDeviceManager.SavePinnedDevices();
        }

        private void InMinusButton_Click(object sender, EventArgs e)
        {
            var removedIndex = InList.SelectedIndex;
            if (removedIndex > -1)
            {
                InList.Items.RemoveAt(removedIndex);
                _audioDeviceManager.PinnedInputDevices.RemoveAt(removedIndex);
            }
            else
            {
                removedIndex = InCommList.SelectedIndex;
                if (removedIndex > -1)
                {
                    InCommList.Items.RemoveAt(removedIndex);
                    _audioDeviceManager.PinnedInputCommDevices.RemoveAt(removedIndex);
                }
            }
            _audioDeviceManager.SavePinnedDevices();
        }

        private void OutUpButton_Click(object sender, EventArgs e)
        {
            var listBoxToSwap = OutList.SelectedIndex > -1 ? OutList : OutCommList;
            var devListToSwap = OutList.SelectedIndex > -1 ? _audioDeviceManager.PinnedOutputDevices : _audioDeviceManager.PinnedOutputCommDevices;
            var index = listBoxToSwap.SelectedIndex;
            Swap(listBoxToSwap, index, index - 1);
            Swap(devListToSwap, index, index - 1);
            listBoxToSwap.SelectedIndex = index - 1;
            _audioDeviceManager.SavePinnedDevices();
        }

        private void OutDownButton_Click(object sender, EventArgs e)
        {
            var listBoxToSwap = OutList.SelectedIndex > -1 ? OutList : OutCommList;
            var devListToSwap = OutList.SelectedIndex > -1 ? _audioDeviceManager.PinnedOutputDevices : _audioDeviceManager.PinnedOutputCommDevices;
            var index = listBoxToSwap.SelectedIndex;
            Swap(listBoxToSwap, index, index + 1);
            Swap(devListToSwap, index, index + 1);
            listBoxToSwap.SelectedIndex = index + 1;
            _audioDeviceManager.SavePinnedDevices();
        }

        private void InUpButton_Click(object sender, EventArgs e)
        {
            var listBoxToSwap = InList.SelectedIndex > -1 ? InList : InCommList;
            var devListToSwap = InList.SelectedIndex > -1 ? _audioDeviceManager.PinnedInputDevices : _audioDeviceManager.PinnedInputCommDevices;
            var index = listBoxToSwap.SelectedIndex;
            Swap(listBoxToSwap, index, index - 1);
            Swap(devListToSwap, index, index - 1);
            listBoxToSwap.SelectedIndex = index - 1;
            _audioDeviceManager.SavePinnedDevices();
        }

        private void InDownButton_Click(object sender, EventArgs e)
        {
            var listBoxToSwap = InList.SelectedIndex > -1 ? InList : InCommList;
            var devListToSwap = InList.SelectedIndex > -1 ? _audioDeviceManager.PinnedInputDevices : _audioDeviceManager.PinnedInputCommDevices;
            var index = listBoxToSwap.SelectedIndex;
            Swap(listBoxToSwap, index, index + 1);
            Swap(devListToSwap, index, index + 1);
            listBoxToSwap.SelectedIndex = index + 1;
            _audioDeviceManager.SavePinnedDevices();
        }
        #endregion

        #region Device List Selection Handlers
        private void OutList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OutList.SelectedIndex > -1) OutCommList.SelectedIndex = -1;
            OutMinusButton.Enabled = OutList.SelectedIndex != -1;
            OutUpButton.Enabled = OutList.SelectedIndex > 0;
            OutDownButton.Enabled = OutList.SelectedIndex < OutList.Items.Count - 1 && OutList.SelectedIndex != -1;
        }

        private void OutCommList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OutCommList.SelectedIndex > -1) OutList.SelectedIndex = -1;
            OutMinusButton.Enabled = OutCommList.SelectedIndex != -1;
            OutUpButton.Enabled = OutCommList.SelectedIndex > 0;
            OutDownButton.Enabled = OutCommList.SelectedIndex < OutCommList.Items.Count - 1 && OutCommList.SelectedIndex != -1;
        }

        private void InList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InList.SelectedIndex > -1) InCommList.SelectedIndex = -1;
            InMinusButton.Enabled = InList.SelectedIndex != -1;
            InUpButton.Enabled = InList.SelectedIndex > 0;
            InDownButton.Enabled = InList.SelectedIndex < InList.Items.Count - 1 && InList.SelectedIndex != -1;
        }

        private void InCommList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InCommList.SelectedIndex > -1) InList.SelectedIndex = -1;
            InMinusButton.Enabled = InCommList.SelectedIndex != -1;
            InUpButton.Enabled = InCommList.SelectedIndex > 0;
            InDownButton.Enabled = InCommList.SelectedIndex < InCommList.Items.Count - 1 && InCommList.SelectedIndex != -1;
        }
        #endregion

        #region UI Helpers
        private void OutCommCheck_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CommOut = OutCommCheck.Checked;
            Properties.Settings.Default.Save();
            if (OutCommCheck.Checked)
            {
                ShowRightCol(OutListLayoutPanel);
                ShowRightCol(OutLabelLayoutPanel);
            }
            else
            {
                HideRightCol(OutListLayoutPanel);
                HideRightCol(OutLabelLayoutPanel);
            }
        }

        private void InCommCheck_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CommIn = InCommCheck.Checked;
            Properties.Settings.Default.Save();
            if (InCommCheck.Checked)
            {
                ShowRightCol(InListLayoutPanel);
                ShowRightCol(InLabelLayoutPanel);
            }
            else
            {
                HideRightCol(InListLayoutPanel);
                HideRightCol(InLabelLayoutPanel);
            }
        }

        private static void ShowRightCol(TableLayoutPanel panel)
        {
            panel.ColumnStyles[0].Width = 50;
            panel.ColumnStyles[1].Width = 50;
        }

        private static void HideRightCol(TableLayoutPanel panel)
        {
            panel.ColumnStyles[0].Width = 100;
            panel.ColumnStyles[1].Width = 0;
        }
        #endregion

        #region List/Device Utility
        public static void Swap(ListBox list, int indexA, int indexB)
        {
            var itemA = list.Items[indexA];
            var itemB = list.Items[indexB];
            list.Items[indexA] = itemB;
            list.Items[indexB] = itemA;
        }

        public static void Swap(List<AudioDevice> list, int indexA, int indexB)
        {
            var itemA = list[indexA];
            var itemB = list[indexB];
            list[indexA] = itemB;
            list[indexB] = itemA;
        }
        #endregion

        #region Tray and Window Events
        private void TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void TrayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TrayIcon.ContextMenuStrip?.Show(Cursor.Position);
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized || HideTray)
            {
                Hide();
                if (HideTray)
                    TrayIcon.Visible = false;
            }
        }
        #endregion

        #region Auto-Launch
        private void AutoLaunchCheckbox_CheckedChanged(object? sender, EventArgs e)
        {
            var hide = ModifierKeys.HasFlag(Keys.Shift);
            if (hide && AutoLaunchCheckbox.Checked)
            {
                var response = MessageBox.Show(
                    "Using this option will launch AudioPin on startup but completely hidden.\r\n"
                    + "You can access the UI by re-launching the application normally, which will stop the hidden instance.\r\n"
                    + "If you wish to manually launch in hidden mode use the /hide command-line option.",
                    "Hidden Auto-Start",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information);
                if (response != DialogResult.OK)
                {
                    AutoLaunchCheckbox.Checked = false;
                    return;
                }
            }

            var autoRunRegistry = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (AutoLaunchCheckbox.Checked && autoRunRegistry != null)
            {
                autoRunRegistry.SetValue("AudioPin", LaunchCommand(hide));
            }
            else
            {
                autoRunRegistry?.DeleteValue("AudioPin", false);
            }
        }

        private static bool AutoLaunchEnabled()
        {
            var autoRunRegistry = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", false);
            if (autoRunRegistry != null)
            {
                var autoRunValue = autoRunRegistry.GetValue("AudioPin");
                return autoRunValue != null && (autoRunValue.ToString()?.Contains(LaunchCommand()) ?? false);
            }
            return false;
        }

        static private string LaunchCommand(bool hide = false)
        {
            var process = Process.GetCurrentProcess();
            if (process.MainModule != null)
            {
                return $"{process.MainModule.FileName} /min{(hide ? " /hide" : string.Empty)}";
            }
            return string.Empty;
        }
        #endregion

        #region Update and Links
        private async void CheckUpdate()
        {
            try
            {
                string releasesResponse;

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://api.github.com/repos/xjph/AudioPin/releases"),
                    Headers = { { "User-Agent", "Xjph/AudioPin" } }
                };

                releasesResponse = await (await _httpClient.SendAsync(request)).Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(releasesResponse))
                {
                    var releases = System.Text.Json.JsonDocument.Parse(releasesResponse).RootElement.EnumerateArray();

                    Version? latestVersion = null;
                    string latestVersionUrl = string.Empty;

                    foreach (var release in releases)
                    {
                        var tag = release.GetProperty("tag_name").ToString();
                        var verstrings = tag[1..].Split('.');
                        var ver = verstrings.Select(verString => { _ = int.TryParse(verString, out int ver); return ver; }).ToArray();
                        if (ver.Length == 3 || ver.Length == 4)
                        {
                            Version githubVersion = new(ver[0], ver[1], ver[2], ver.Length == 3 ? 0 : ver[3]);
                            if (latestVersion == null || githubVersion > latestVersion)
                            {
                                latestVersion = githubVersion;
                                latestVersionUrl = release.GetProperty("html_url").ToString();
                            }
                        }
                    }

                    if (latestVersion > System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version)
                    {
                        UpdateLink.Visible = true;
                        UpdateLink.Enabled = true;
                        UpdateLink.LinkClicked += (_, _) =>
                        {
                            OpenURL(latestVersionUrl);
                        };
                    }
                }
            }
            catch
            {
                // Just let update check fail silently
            }
        }

        private void DonateLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenURL("https://ko-fi.com/vithigar");
        }

        private void GithubLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenURL("https://github.com/Xjph/AudioPin");
        }
        #endregion

        #region Utility
        private static void OpenURL(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open URL: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
