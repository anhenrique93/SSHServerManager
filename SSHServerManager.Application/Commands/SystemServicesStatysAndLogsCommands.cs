using SSHServerManager.Application.Interfaces;

namespace SSHServerManager.Application.Commands
{
    public record RunningServices(IClient Client) : ICommand
    {
        public string Execute() => Client.RunningServices();
    }

    public record ActiveServices(IClient Client) : ICommand
    {
        public string Execute() => Client.ActiveServices();
    }

    public record FailedServices(IClient Client) : ICommand
    {
        public string Execute() => Client.FailedServices();
    }

    public record ExecutingWithSystem(IClient Client) : ICommand
    {
        public string Execute() => Client.ExecutingWithSystem();
    }

    public record ServiceStatus(IClient Client, string serviceName) : ICommand
    {
        public string Execute() => Client.ServiceStatus(serviceName);
    }

    public record ServiceLogs(IClient Client, string serviceName, int lines = 100) : ICommand
    {
        public string Execute() => Client.ServiceLogs(serviceName, lines);
    }
}
