using System.Diagnostics;

namespace AudioPin
{
    internal static class AudioPin
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            foreach (var process in processes)
            {
                if (process.Id != Environment.ProcessId)
                    process.Kill();
            }

            try
            {
                Properties.Settings.Default.Upgrade();
            }
            catch
            {
                MessageBox.Show(
                    "Unable to load saved settings from previous version. Defaults have been restored.",
                    "Settings Load Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ApplicationConfiguration.Initialize();
            var audioManager = new AudioDeviceManager();
            var mainForm = new MainForm(audioManager);
            mainForm.HideTray = args.Contains("/hide");
            if (args.Length > 0 && args.Contains("/min"))
            {
                mainForm.WindowState = FormWindowState.Minimized;
            }

            Application.Run(mainForm);
        }
    }
}