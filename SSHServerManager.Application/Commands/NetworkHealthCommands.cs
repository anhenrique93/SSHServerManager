using SSHServerManager.Application.Interfaces;

namespace SSHServerManager.Application.Commands
{
    public record InterfaceIps(IClient Client) : ICommand
    {
        public string Execute() => Client.InterfaceIps();
    }

    public record DefaultRoute(IClient Client) : ICommand
    {
        public string Execute() => Client.DefaultRoute();
    }

    public record ServerDns(IClient Client) : ICommand
    {
        public string Execute() => Client.ServerDns();
    }

    public record NetworkStatistic(IClient Client) : ICommand
    {
        public string Execute() => Client.NetworkStatistic();
    }

    public record SpeedTest(IClient Client) : ICommand
    {
        public string Execute() => Client.SpeedTest();
    }


}
