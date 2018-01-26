using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WindowsUpdateMonitor
{
    [SuppressMessage("ReSharper", "LocalizableElement")]
    public partial class FormMain : Form
    {
        private bool _checkPending;
        private bool _checkRunning;
        private bool _closing;
        private readonly List<RegistryKeyWatcher> _registryKeyWatchers = new List<RegistryKeyWatcher>();

        private void AppendLogText(string message)
        {
            if (CheckAndInvokeIfRequired(() => AppendLogText(message)))
            {
                return;
            }

            var logMessage = $"{DateTime.Now} - {message}";

            Debug.WriteLine(logMessage);
            TextBoxLog.AppendText($"{logMessage}\r\n");
        }

        private void ButtonCheckWindowsUpdateSettings_Click(object sender, EventArgs e)
        {
            CheckWindowsUpdateSettings();
        }

        private bool CheckAndInvokeIfRequired(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(action);
                return true;
            }

            return false;
        }

        private async void CheckWindowsUpdateSettings()
        {
            StopRegistryKeyWatchers();

            _checkRunning = true;
            _checkPending = false;

            AppendLogText("Checking Windows Update settings");

            var runWindowsUpdate = false;

            var key1 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate", true);
            if (key1?.GetValue("DisableDualScan") != null)
            {
                AppendLogText("Deleting 'DisableDualScan' registry key");

                key1.DeleteValue("DisableDualScan");
                runWindowsUpdate = true;
            }

            var key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU", true);
            if (key2?.GetValue("NoAutoUpdate") != null)
            {
                AppendLogText("Deleting 'NoAutoUpdate' registry key");

                key2.DeleteValue("NoAutoUpdate");
                runWindowsUpdate = true;
            }

            if (runWindowsUpdate)
            {
                AppendLogText("Executing Windows Update check");

                var windowsUpdate = new WUApiLib.AutomaticUpdatesClass();
                if (!windowsUpdate.ServiceEnabled)
                {
                    windowsUpdate.EnableService();
                    await Task.Delay(1000);
                }

                // 0x8024A000 - No AU available. Error codes here: https://support.microsoft.com/en-us/help/938205/windows-update-error-code-list
                try
                {
                    windowsUpdate.DetectNow();
                }
                catch (Exception e)
                {
                    AppendLogText($"Error executing Windows Update check: {e}");
                }
            }

            _checkRunning = false;

            StartRegistryKeyWatchers();
        }

        private void ExitApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _closing = true;

            Close();
        }

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_closing || e.CloseReason == CloseReason.TaskManagerClosing || e.CloseReason == CloseReason.WindowsShutDown)
            {
                return;
            }

            e.Cancel = true;
            WindowState = FormWindowState.Minimized;
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            var isMinimized = WindowState == FormWindowState.Minimized;

            NotifyIconMain.Visible = isMinimized;
            ShowInTaskbar = !isMinimized;

            Application.DoEvents();
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            CheckWindowsUpdateSettings();
        }

        private void NotifyIconMain_DoubleClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }

        private void RegistryKeyChanged(object sender, RegistryKeyChangedEventArgs e)
        {
            AppendLogText($"{e}");

            if (_checkPending || _checkRunning)
            {
                return;
            }

            _checkPending = true;

            Task.Delay(TimeSpan.FromSeconds(5)).ContinueWith((t) => CheckWindowsUpdateSettings());
        }

        private void StartRegistryKeyWatchers()
        {
            StopRegistryKeyWatchers();

            _registryKeyWatchers.Add(new RegistryKeyWatcher(Registry.LocalMachine, @"SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate"));
            _registryKeyWatchers.Add(new RegistryKeyWatcher(Registry.LocalMachine, @"SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU"));

            foreach (var registryWatcher in _registryKeyWatchers)
            {
                registryWatcher.RegistryKeyChanged += RegistryKeyChanged;
                registryWatcher.Start();
            }
        }

        private void StopRegistryKeyWatchers()
        {
            foreach (var registryWatcher in _registryKeyWatchers)
            {
                registryWatcher.RegistryKeyChanged -= RegistryKeyChanged;
                registryWatcher.Dispose();
            }

            _registryKeyWatchers.Clear();
        }
    }
}
