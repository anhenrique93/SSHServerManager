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
}
