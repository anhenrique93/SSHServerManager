using ConnectionManager;
using SSHServerManager.Application.Interfaces;
using SSHServerManager.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHServerManager.Apresentation
{
    public static class ConsoleInterface
    {
        public static string ServerAddress { get; private set; } = string.Empty;
        public static int? ServerPort { get; private set; } = 22;
        public static string UserName { get; private set; } = string.Empty;
        public static string Password { get; private set; } = string.Empty;
        public static string? KeyPath { get; private set; } = string.Empty;

        public static void GetServerCredentials()
        {
            GetServerAddres();
            GetServerPort();
            GetUserName();
            GetKeyPath();
            GetPassword();
        }

        private static void GetServerAddres()
        {
            while (string.IsNullOrEmpty(ServerAddress))
            {
                Console.Write("Enter server address: ");
                ServerAddress = Console.ReadLine()!;
            }
        }

        private static void GetUserName()
        {
            while (string.IsNullOrEmpty(UserName))
            {
                Console.Write("Enter username: ");
                UserName = Console.ReadLine()!;
            }
        }

        private static void GetPassword()
        {
            while (string.IsNullOrEmpty(Password))
            {
                Console.Write("Enter password: ");
                Password = Console.ReadLine()!;
            }
        }

        private static void GetKeyPath()
        {
            Console.Write("Enter path to private key (leave empty if using password): ");
            var keyPath = Console.ReadLine();

            if (!string.IsNullOrEmpty(keyPath))
            {
                KeyPath = keyPath;
            }
        }

        private static void GetServerPort()
        {
            Console.Write("Enter server port (default 22): ");
            var serverPort = Console.ReadLine();

            if (!string.IsNullOrEmpty(serverPort))
            {
                int.TryParse(serverPort, out int port);
                ServerPort = port == 0 ? 22 : port;
            }
        }
    }
}
