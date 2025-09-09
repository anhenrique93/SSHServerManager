using SSHServerManager.Application.Interfaces;

namespace SSHServerManager.Application.Commands
{
    public record SmartStatus(IClient Client, string disk) : ICommand
    {
        public string Execute() => Client.SmartStatus(disk);
    }

    public record SmartInfo(IClient Client, string disk) : ICommand
    {
        public string Execute() => Client.SmartInfo(disk);
    }

    public record DiskPartitions(IClient Client) : ICommand
    {
        public string Execute() => Client.DiskPartitions();
    }

    public record DiskUsage(IClient Client) : ICommand
    {
        public string Execute() => Client.DiskUsage();
    }
}
