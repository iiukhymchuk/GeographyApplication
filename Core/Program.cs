using Google.Maps;
using System;
using System.Windows.Forms;

namespace Core
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // custom settings
            AppDomain.CurrentDomain.SetData("DataDirectory", Settings.GetAppDataDirectory());
            GoogleSigned.AssignAllServices(new GoogleSigned(Settings.API_KEY));
            // winforms settings
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GeographyForm());
        }
    }
}
