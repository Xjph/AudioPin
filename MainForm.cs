using CoreAudioApi;
using System.Diagnostics;

namespace AudioPin
{
    public partial class MainForm : Form
    {
        private AudioDeviceManager _audioDeviceManager;

        public MainForm(AudioDeviceManager audioDeviceManager)
        {
            _audioDeviceManager = audioDeviceManager;
            InitializeComponent();
            Icon = Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location);
            TrayIcon.Icon = Icon;
            TrayIcon.ContextMenuStrip = new TrayMenu(this);
            LoadSettings();
        }

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

        private static void Swap(ListBox list, int indexA, int indexB)
        {
            var itemA = list.Items[indexA];
            var itemB = list.Items[indexB];
            list.Items[indexA] = itemB;
            list.Items[indexB] = itemA;
        }

        private static void Swap(List<AudioDevice> list, int indexA, int indexB)
        {
            var itemA = list[indexA];
            var itemB = list[indexB];
            list[indexA] = itemB;
            list[indexB] = itemA;
        }

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

        private void DonateLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://ko-fi.com/vithigar",
                UseShellExecute = true
            });
        }

        private void AutoLaunchCheckbox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void GithubLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
