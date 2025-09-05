namespace SSHServerManager.Application.Interfaces
{
    public interface IClient
    {
        bool IsConnected { get; }
        bool TryConnection(out string? error);
        void Disconnect();
        void Dispose();

        // System
        public string WhoAmI();
        public string UpdatePackagesList();
        public string UpgradePackages();
        public string Reboot();

        // System state
        public string HostName();
        public string Uptime();
        public string LastBoot();
        public string MemoryInfo();
        public string DiskUsage();
        public string Nodes();
        public string DiskPartitions();
        public string Processes();
        public string FailedServices();
        public string ActiveServices();
        public string WarningsErrorsLogs();

        // Services status and logs
        public string ServiceStatus(string serviceName);
        public string ServiceLogs(string serviceName, int lines = 100);

        // Disks health
        public string SmartStatus(string disk);
        public string SmartInfo(string disk);

        // Network health
        public string InterfaceIps();
        public string DefaultRoute();
        public string ServerDns();
        public string NetworkStatistic();
        public string SpeedTest();
        public string AtualDir();

        // Network connections
        public string ListeningPorts();
        public string EstablishedConnections();
        public string AllConnections();
        public string FirewallStatus();
        public string FirewallLogs(int lines = 100);
        public string ICMP();
        public string DNSResolution(string domain = "google.com");
        public string Traceroute(string domain = "google.com");
    }
}