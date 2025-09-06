using SSHServerManager.Application.Interfaces;

namespace SSHServerManager.Application.Commands
{
    public record HostName(IClient Client) : ICommand
    {
        public string Execute() => Client.HostName();
    }

    public record Uptime(IClient Client) : ICommand
    {
        public string Execute() => Client.Uptime();
    }

    public record LastBoot(IClient Client) : ICommand
    {
        public string Execute() => Client.LastBoot();
    }

    public record MemoryInfo(IClient Client) : ICommand
    {
        public string Execute() => Client.MemoryInfo();
    }

    public record DiskUsage(IClient Client) : ICommand
    {
        public string Execute() => Client.DiskUsage();
    }

    public record Nodes(IClient Client) : ICommand
    {
        public string Execute() => Client.Nodes();
    }

    public record DiskPartitions(IClient Client) : ICommand
    {
        public string Execute() => Client.DiskPartitions();
    }

    public record Processes(IClient Client) : ICommand
    {
        public string Execute() => Client.Processes();
    }

    public record WarningsErrorsLogs(IClient Client) : ICommand
    {
        public string Execute() => Client.WarningsErrorsLogs();
    }
}
