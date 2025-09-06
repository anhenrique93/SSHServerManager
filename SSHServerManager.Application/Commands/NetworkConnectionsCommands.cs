using SSHServerManager.Application.Interfaces;

namespace SSHServerManager.Application.Commands
{
    public record ListeningPorts(IClient Client) : ICommand
    {
        public string Execute() => Client.ListeningPorts();
    }

    public record EstablishedConnections(IClient Client) : ICommand
    {
        public string Execute() => Client.EstablishedConnections();
    }

    public record AllConnections(IClient Client) : ICommand
    {
        public string Execute() => Client.AllConnections();
    }

    public record FirewallStatus(IClient Client) : ICommand
    {
        public string Execute() => Client.FirewallStatus();
    }

    public record FirewallLogs(IClient Client, int lines = 100) : ICommand
    {
        public string Execute() => Client.FirewallLogs(lines);
    }

    public record ICMP(IClient Client) : ICommand
    {
        public string Execute() => Client.ICMP();
    }

    public record DNSResolution(IClient Client, string domain = "google.com") : ICommand
    {
        public string Execute() => Client.DNSResolution(domain);
    }

    public record Traceroute(IClient Client, string domain = "google.com") : ICommand
    {
        public string Execute() => Client.Traceroute(domain);
    }
}
