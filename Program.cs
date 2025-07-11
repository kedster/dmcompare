// Program.cs - placeholder for . layer
using System;
using System.Windows.Forms;

namespace DMSRuntimeComparer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Enable WinForms visual styles (theme support)
            Application.EnableVisualStyles();

            // Ensure older-style text rendering is disabled
            Application.SetCompatibleTextRenderingDefault(false);

            // Launch the main form
            Application.Run(new UI.Mainform());
        }
    }
}
