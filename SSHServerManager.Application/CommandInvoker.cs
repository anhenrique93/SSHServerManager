using SSHServerManager.Application.Interfaces;

namespace ConnectionManager
{
    public class CommandInvoker
    {
        private readonly List<ICommand> _commands = [];

        public void ExecuteCommand(ICommand command)
        {
            try
            {
                var result = command!.Execute();
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error executing command: {ex.Message}");
            }
        }

        public void RegisterCommand(ICommand command)
        { 
            if (!_commands.Contains(command))
                _commands.Add(command);
        }

        public void ExecuteCommands()
        {
            foreach (var command in _commands)
            {
                ExecuteCommand(command);
            }
        }
    }
}
