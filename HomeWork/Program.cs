using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace HomeWork
{
    internal static class Program
    {
        
        private static List<Command> CreateCommandsList(RequestsSystem requestsSystem, 
            TelephonesSystem telephonesSystem, ClientsSystem clientsSystem) =>
            new List<Command>
            {
                new AllRequestsCommand(requestsSystem, clientsSystem, telephonesSystem),
                new AllTelephonesCommand(requestsSystem, clientsSystem, telephonesSystem),
                new AddRequestCommand(requestsSystem, clientsSystem, telephonesSystem),
                new RemoveRequestCommand(requestsSystem, clientsSystem, telephonesSystem),
                new UpdateRequestCommand(requestsSystem, clientsSystem, telephonesSystem),
                new AddTelephoneCommand(requestsSystem, clientsSystem, telephonesSystem),
                new RemoveTelephoneCommand(requestsSystem, clientsSystem, telephonesSystem),
                new UpdateTelephoneCommand(requestsSystem, clientsSystem, telephonesSystem),

                 new AllClientsCommand(requestsSystem, clientsSystem, telephonesSystem),
                 new AddClientCommand(requestsSystem, clientsSystem, telephonesSystem),
                new RemoveClientCommand(requestsSystem, clientsSystem, telephonesSystem),
                new UpdateClientCommand(requestsSystem, clientsSystem, telephonesSystem),
                new ExitCommand(requestsSystem, clientsSystem, telephonesSystem)
            };

        private static void PrintHelpMessage(List<Command> commands) =>
            Console.WriteLine("Введите одну из команд:\n" +
                              string.Join("\n", commands.Select(command => $" - {command}")));

        public static void Main()
        {
            using var requestsSystem = new RequestsSystem("../../../requests.txt");
            using var telephonesSystem = new TelephonesSystem("../../../telephones.txt");
            using var clientsSystem = new ClientsSystem("../../../clients.txt");
            var commands = CreateCommandsList(requestsSystem, telephonesSystem, clientsSystem);

            PrintHelpMessage(commands);
            while (true)
            {
                try
                {
                    var input = Console.ReadLine();
                    if (input == null)
                    {
                        PrintHelpMessage(commands);
                        continue;
                    }

                    var commandName = new Regex("\\s+").Split(input)[0];
                    var arguments = input
                        .Remove(0, commandName.Length)
                        .Split(Constants.DataSeparator)
                        .Select(a => a.Trim())
                        .Where(a => a.Any())
                        .ToArray();

                    var command = commands.Find(c => c.Name == commandName);
                    if (command == null)
                    {
                        PrintHelpMessage(commands);
                        continue;
                    }

                    if (command.Arguments.Length != arguments.Length)
                    {
                        Console.WriteLine($"Используйте: {command}");
                        continue;
                    }

                    Console.WriteLine(command.Execute(arguments));
                    if (command is ExitCommand) break;
                }
                catch (Exception e) when
                (e is FileNotFoundException ||
                 e is FormatException ||
                 e is IndexOutOfRangeException ||
                 e is ArgumentException)
                {
                    Console.Error.WriteLine(e.Message);
                }
            }
        }
    }
}