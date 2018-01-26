using System;
using System.Collections.ObjectModel;
using System.Management;
using Microsoft.Win32;

namespace WindowsUpdateMonitor
{
    internal class RegistryKeyWatcher: ManagementEventWatcher, IDisposable
    {
        private static ReadOnlyCollection<RegistryKey> _supportedHives;

        /// <summary>
        /// Changes to the HKEY_CLASSES_ROOT and HKEY_CURRENT_USER hives are not supported
        /// by RegistryEvent or classes derived from it, such as RegistryKeyChangeEvent. 
        /// </summary>
        public static ReadOnlyCollection<RegistryKey> SupportedHives
        {
            get
            {
                if (_supportedHives == null)
                {
                    RegistryKey[] supportedHives = {Registry.LocalMachine, Registry.Users, Registry.CurrentConfig};
                    _supportedHives = Array.AsReadOnly(supportedHives);
                }
                return _supportedHives;
            }
        }

        public RegistryKey Hive { get; }
        public string KeyPath { get; }
        public RegistryKey Key { get; }

        public event EventHandler<RegistryKeyChangedEventArgs> RegistryKeyChanged;

        public RegistryKeyWatcher(RegistryKey hive, string keyPath)
        {
            Hive = hive;
            KeyPath = keyPath;
            Key = hive.OpenSubKey(keyPath);

            if (Key != null)
            {
                Query = new WqlEventQuery
                {
                    QueryString = $@"SELECT * FROM RegistryValueChangeEvent WHERE Hive = '{Hive.Name}' AND KeyPath = '{KeyPath}' ",
                    EventClassName = "RegistryKeyChangeEvent",
                    WithinInterval = new TimeSpan(0, 0, 0, 1)
                };

                EventArrived += RegistryWatcher_EventArrived;
            }
            else
            {
                throw new ArgumentException($@"The registry key {Hive.Name}\{KeyPath} does not exist");
            }
        }

        public new void Dispose()
        {
            base.Dispose();
            Key?.Dispose();
        }
        private void RegistryWatcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            RegistryKeyChanged?.Invoke(sender, new RegistryKeyChangedEventArgs(e.NewEvent));
        }
    }
}
