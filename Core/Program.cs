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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GoogleSigned.AssignAllServices(new GoogleSigned(Settings.API_KEY));
            Application.Run(new GeographyForm());
        }
    }
}
