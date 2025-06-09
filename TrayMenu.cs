namespace AudioPin
{
    internal class TrayMenu : ContextMenuStrip
    {
        public TrayMenu(Form mainForm)
        {
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
    }
}
