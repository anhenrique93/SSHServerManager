using Renci.SshNet;
using SSHServerManager.Application.Interfaces;

namespace SSHServerManager.Connection
{
    public class SSHClient : IClient, IDisposable
    {
        private readonly ConnectionInfo _connectionInfo;
        private SshClient? _client;
        private readonly object _clientLock = new object();
        public bool IsConnected => _client?.IsConnected == true;
        private readonly string? _sudoPassword;

        public SSHClient(ConnectionInfo connectionInfo, string? sudoPassword)
        {
            _connectionInfo = connectionInfo;
            _sudoPassword = sudoPassword;
        }

        public bool TryConnection(out string? error)
        {
            lock (_clientLock)
            {
                error = null;

                // Close last connection if exists
                _client?.Dispose();
                _client = new SshClient(_connectionInfo);

                try
                {
                    _client.Connect();
                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    _client.Dispose();
                    _client = null;
                    return false;
                }
            }
        }

        public void Disconnect()
        {
            lock (_clientLock)
            {
                if (_client?.IsConnected == true)
                {
                    _client?.Dispose();
                    _client = null;
                }
            }
        }

        public void Dispose() => Disconnect();

        public string Run(string command, int timeoutSeconds = 30)
        {
            if (!IsConnected) throw new InvalidOperationException("Not connected!");

            var trimmed = command.TrimStart();
            if (trimmed.StartsWith("sudo ", StringComparison.Ordinal))
            {
                if (string.IsNullOrEmpty(_sudoPassword))
                    throw new InvalidOperationException("Este comando requer sudo, mas não foi fornecida sudoPassword.");

                var inner = trimmed.Substring("sudo ".Length);

                static string EscapeSingleQuotes(string s) => s.Replace("'", "'\"'\"'");

                var pwd = EscapeSingleQuotes(_sudoPassword);
                command = $"printf '%s\\n' '{pwd}' | sudo -S -p '' {inner}";
            }

            using var cmd = _client!.CreateCommand(command);
            cmd.CommandTimeout = TimeSpan.FromSeconds(timeoutSeconds);

            var stdout = cmd.Execute();
            if (cmd.ExitStatus != 0) throw new Exception(cmd.Error);
            return stdout.TrimEnd();
        }

        // System
        public string WhoAmI() => Run("whoami");
        public string UpdatePackagesList() => Run("sudo apt update 2>&1", timeoutSeconds: 180);
        public string UpgradePackages() => Run("sudo apt upgrade -y 2>&1", timeoutSeconds: 180);
        public string Reboot() => Run("sudo reboot, timeoutSeconds: 180");

        // System information and health
        public string HostName() => Run("hostnamectl");
        public string Uptime() => Run("uptime");
        public string LastBoot() => Run("who -b");
        public string MemoryInfo() => Run("free -h");
        public string DiskUsage() => Run("df -h");
        public string Nodes() => Run("df -i");
        public string DiskPartitions() => Run("lsblk -f");
        public string Processes() => Run("htop");
        public string FailedServices() => Run("systemctl --failed");
        public string ActiveServices() => Run("systemctl list-units --type=service --state=active");
        public string WarningsErrorsLogs() => Run("journalctl -p warning -b");

        // Services status and logs
        public string ServiceStatus(string serviceName) => Run($"systemctl status {serviceName}");
        public string ServiceLogs(string serviceName, int lines = 100) => Run($"journalctl -u {serviceName} -n {lines} --no-pager");

        // Disks health
        public string SmartStatus(string disk) => Run($"sudo smartctl -H {disk}", timeoutSeconds: 180);
        public string SmartInfo(string disk) => Run($"sudo smartctl -i {disk}", timeoutSeconds: 180);

        // Network health
        public string InterfaceIps() => Run("ip -brief address ");
        public string DefaultRoute() => Run("ip route");
        public string ServerDns() => Run("resolvectl status");
        public string NetworkStatistic() => Run("ip -s link");
        public string SpeedTest() => Run("speedtest-cli --simple", timeoutSeconds: 120);
        public string AtualDir() => Run("pwd");

        // Network connections
        public string ListeningPorts() => Run("ss -tulpn");
        public string EstablishedConnections() => Run("ss -s");
        public string AllConnections() => Run("ss -tunap");
        public string FirewallStatus() => Run("sudo ufw status verbose", timeoutSeconds: 180);
        public string FirewallLogs(int lines = 100) => Run($"sudo tail -n {lines} /var/log/ufw.log", timeoutSeconds: 180);
        public string ICMP() => Run("ping -c 4 1.1.1.1 ");
        public string DNSResolution(string domain = "google.com") => Run($"ping {domain}");
        public string Traceroute(string domain = "google.com") => Run($"traceroute {domain}");



    }
}
