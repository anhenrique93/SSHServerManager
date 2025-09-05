using ConnectionManager;
using ConnectionManager.Commands;

var info = SshConnectionInfo.ForPrivateKey( "192.168.1.32", "henrique", @"C:\Users\Henrique\.ssh\id_rsa");

SSHClient client = new SSHClient(info, "BRAGA33prtcs16!");

if (!client.TryConnection(out var error))
{
    Console.Error.WriteLine($"Falha na ligação SSH: {error}");
    return;
}

try
{
    var invoker = new CommandInvoker();
    invoker.RegisterCommand(new WhoAmI(client));
    //invoker.RegisterCommand(new UpdatePackageList(client));
    //invoker.RegisterCommand(new UpgradePackages(client));
    invoker.ExecuteCommands();
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Erro ao executar comando: {ex.Message}");
}

/*
 Lightweight .NET (C#) SSH helper built on SSH.NET. Quickly connect via password or private key and run remote commands, with optional Command Pattern support (commands + invoker) for queueable, testable actions.
 */