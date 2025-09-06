using SSHServerManager.Application.Interfaces;

namespace SSHServerManager.Application.Commands
{
    public record WhoAmI(IClient Client) : ICommand
    {
        public string Execute() => Client.WhoAmI();
    }

    public record UpdatePackageList(IClient Client) : ICommand
    {
        public string Execute() => Client.UpdatePackagesList();
    }

    public record UpgradePackages(IClient Client) : ICommand
    {
        public string Execute() => Client.UpgradePackages();
    }

    public record Reboot(IClient Client) : ICommand
    {
        public string Execute() => Client.Reboot();
    }
}
