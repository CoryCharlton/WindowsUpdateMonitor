using System;
using System.Threading;
using System.Windows.Forms;

namespace WindowsUpdateMonitor
{
    static class Program
    {
#if DEBUG
        private static readonly Mutex SingleInstance = new Mutex(true, "WindowsUpdateMonitor_{2684D663-B8F4-4E15-B479-F5F4C7EFE487}");
#else
        private static readonly Mutex SingleInstance = new Mutex(true, "WindowsUpdateMonitor_{80744FB0-1515-45E6-80BA-32548033D92F}");
#endif

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!SingleInstance.WaitOne(TimeSpan.Zero, true))
            {
                MessageBox.Show("Another instance of the application is already running.", "Windows Update Monitor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
            }
            finally
            {
                SingleInstance.ReleaseMutex();
            }
        }
    }
}
