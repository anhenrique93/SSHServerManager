using ConnectionManager;
using SSHServerManager.Application.Commands;
using SSHServerManager.Application.Interfaces;
using SSHServerManager.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHServerManager.Apresentation
{
    public class ConsoleInterface
    {
        public static string ServerAddress { get; private set; } = string.Empty;
        public static int? ServerPort { get; private set; } = 22;
        public static string UserName { get; private set; } = string.Empty;
        public static string Password { get; private set; } = string.Empty;
        public static string? KeyPath { get; private set; } = string.Empty;

        private readonly CommandInvoker _invoker;
        private readonly IClient _client;

        public ConsoleInterface(CommandInvoker invoker, IClient client)
        {
            _invoker = invoker;
            _client = client;
        }

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
            do
            {
                Console.Write("Enter server address: ");
                ServerAddress = Console.ReadLine()!;
            } while (string.IsNullOrEmpty(ServerAddress)) ;
            
        }

        private static void GetUserName()
        {
            do
            {
                Console.Write("Enter username: ");
                UserName = Console.ReadLine()!;
            } while (string.IsNullOrEmpty(UserName));
            
        }

        private static void GetPassword()
        {   
            do
            {
                Console.Write("Enter password: ");
                Password = Console.ReadLine()!;
            } while (string.IsNullOrEmpty(Password)) ;
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

        public void ShowMenu()
        {
            string? choice;
            do
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. DiskHealth");
                Console.WriteLine("2. Network Connections");
                Console.WriteLine("3. Network Health");
                Console.WriteLine("4. System");
                Console.WriteLine("5. System Information");
                Console.WriteLine("6. System Services");
                Console.Write("Choose an option: ");
                choice = Console.ReadLine();
                
                if (string.IsNullOrEmpty(choice))
                {
                    Console.WriteLine("Invalid choice.");
                    Console.Clear();
                }

            } while (string.IsNullOrEmpty(choice));
            
            ExecuteMenuChoice(choice);
        }

        private void ExecuteMenuChoice(string choice)
        {
            Console.Clear();
            switch (choice)
            {
                case "1":
                    DiskHealthMenu();
                    break;
                case "2":
                    // Execute Network Connections command
                    break;
                case "3":
                    // Execute Network Health command
                    break;
                case "4":
                    // Execute System command
                    break;
                case "5":
                    // Execute System Information command
                    break;
                case "6":
                    // Execute System Services command
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        private void DiskHealthMenu() 
        {
            string? choice;
            do
            {
                Console.WriteLine("Disks:");
                Console.WriteLine("1. Partitions");
                Console.WriteLine("2. Usage");
                Console.WriteLine("3. Smart info");
                Console.WriteLine("4. Smart Status");
                Console.WriteLine("5. Back");
                Console.Write("Choose an option: ");
                choice = Console.ReadLine();

                if (string.IsNullOrEmpty(choice))
                {
                    Console.WriteLine("Invalid choice.");
                    Console.Clear();
                }

            } while (string.IsNullOrEmpty(choice));

            Console.Clear();

            switch (choice) 
            {
                case "1":
                    _invoker.ExecuteCommand(new DiskPartitions(_client));
                    break;
                case "2":
                    _invoker.ExecuteCommand(new DiskUsage(_client));
                    break;
                case "3":
                    Console.Write("Enter disk (e.g., /dev/sda): ");
                    var diskInfo = Console.ReadLine();
                    if (!string.IsNullOrEmpty(diskInfo))
                        _invoker.ExecuteCommand(new SmartInfo(_client, diskInfo));
                    else
                        Console.WriteLine("Invalid disk.");
                    break;
                case "4":
                    Console.Write("Enter disk (e.g., /dev/sda): ");
                    var diskStatus = Console.ReadLine();
                    if (!string.IsNullOrEmpty(diskStatus))
                        _invoker.ExecuteCommand(new SmartStatus(_client, diskStatus));
                    else
                        Console.WriteLine("Invalid disk.");
                    break;
                case "5":
                    ShowMenu();
                    break;
            }
            DiskHealthMenu();
        }
    }
}
