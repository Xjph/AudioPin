namespace AudioPin
{
    internal static class AudioPin
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            var audioManager = new AudioDeviceManager();
            Application.Run(new MainForm(audioManager));
        }
    }
}