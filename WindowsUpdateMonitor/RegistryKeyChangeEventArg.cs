using System;
using System.Management;

namespace WindowsUpdateMonitor
{
    internal class RegistryKeyChangedEventArgs : EventArgs
    {
        public string Hive { get; }
        public string Key { get; }
        public DateTime TimeCreated { get; }

        public RegistryKeyChangedEventArgs(ManagementBaseObject arrivedEvent)
        {

            Hive = arrivedEvent.Properties["Hive"].Value as string;
            Key = arrivedEvent.Properties["KeyPath"].Value as string;
            TimeCreated = new DateTime((long)(ulong)arrivedEvent.Properties["TIME_CREATED"].Value, DateTimeKind.Utc).AddYears(1600);
        }

        public override string ToString()
        {
            return $"{Hive}\\{Key} changed";
        }
    }
}
