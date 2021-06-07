using GymSys.Data;
using GymSys.Data.Models;
using System;
using System.Linq;
using System.Text;

namespace GymSys
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var db = new GymSysContext();

            bool isCommandFinished = false;
            PrintLogo();
            PrintWelcomeMessage();

            while (true)
            {
                Console.WriteLine();
                Console.Write("Enter command: ");
                string command = Console.ReadLine().ToLower();
                string[] cmdArg = command.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
                Console.WriteLine();

                if (cmdArg[0] == "help")
                {
                    PrintCommandHelpMessage();
                    // return;
                }
                // ADD COMMANDS 
                else if (cmdArg[0] == "add")
                {
                    if (cmdArg[1] == "client")
                    {
                        Console.Write("Please enter first name: ");
                        string firstName = Console.ReadLine();

                        Console.Write("Please enter middle name: ");
                        string middleName = Console.ReadLine();

                        Console.Write("Please enter last name: ");
                        string lastName = Console.ReadLine();

                        Console.Write("Please enter phone number: ");
                        string phoneNumber = Console.ReadLine();

                        Console.Write("Please enter email address: ");
                        string email = Console.ReadLine();

                        Console.WriteLine("Choise one of the cities:");
                        var allTowns = db.Towns.Select(x => new
                        {
                            x.TownId,
                            x.Name,
                        })
                            .ToList();
                        var sb = new StringBuilder();
                        foreach (var item in allTowns.OrderBy(x => x.Name))
                        {
                            sb.AppendLine($"{item.TownId} => {item.Name}");
                        }

                        string result = sb.ToString().TrimEnd();
                        Console.WriteLine(result);
                        Console.Write("Enter the city Id : ");
                        int sId = int.Parse(Console.ReadLine());

                        Client currentClient = new Client()
                        {
                            FirstName = firstName,
                            MiddleName = middleName,
                            LastName = lastName,
                            PhoneNumber = phoneNumber,
                            EmailAddress = email,
                            TownId = sId,
                        };
                        db.Clients.Add(currentClient);

                        var currentCity = allTowns.FirstOrDefault(x => x.TownId == sId);
                        Console.WriteLine($"{currentClient.FirstName} {currentClient.MiddleName} {currentClient.LastName}" +
                            $" with phone number {currentClient.PhoneNumber} with email {currentClient.EmailAddress} in city {currentCity.Name} is added to db");

                        db.SaveChanges();
                    }
                    else if (cmdArg[1] == "town")
                    {
                        Console.Write("Please enter the name: ");
                        string newName = Console.ReadLine();
                        Town town = new Town
                        {
                            Name = newName
                        };
                        Console.WriteLine($"The new town is {newName}");
                        Console.WriteLine();
                        Console.WriteLine("Do you want to save changes ? Press 'Y' for YES or 'N' for NO");
                        while (true)
                        {
                            command = Console.ReadLine().ToLower();
                            if (command == "y")
                            {
                                db.Towns.Add(town);
                                db.SaveChanges();
                                Console.WriteLine($"Succsessfull add in database");
                                isCommandFinished = true;

                            }
                            else if (command == "n")
                            {
                                Console.WriteLine("Is canceled. Please try again.");

                            }

                            if (isCommandFinished)
                            {
                                isCommandFinished = false;
                                break;
                            }
                        }
                    }
                }
                else if (cmdArg[0] == "delete")
                {
                    if (cmdArg[1] == "client")
                    {
                        Console.WriteLine("Loading... please wait");
                        Console.WriteLine();
                        var allClients = db.Clients.Select(x => new
                        {
                            x.FirstName,
                            x.MiddleName,
                            x.LastName,
                            x.ClientId,
                            TownName = x.Town.Name
                        })
                            .OrderBy(x => x.FirstName)
                            .ToList();

                        var sb = new StringBuilder();
                        foreach (var c in allClients)
                        {
                            sb.AppendLine($"{c.ClientId} => {c.FirstName} {c.MiddleName} {c.LastName}  |  {c.TownName}");
                        }
                        Console.WriteLine(sb);
                        Console.WriteLine();
                        Console.Write($"Choice id to delete: ");
                        int idToDelete = int.Parse(Console.ReadLine());

                        Client clientToDelete = db.Clients.FirstOrDefault(x => x.ClientId == idToDelete);
                        if (isNull(clientToDelete))
                        {
                            Console.WriteLine($"Client not exist");
                        }
                        else
                        {
                            Console.WriteLine($"Client {clientToDelete.FirstName} {clientToDelete.LastName} with id {clientToDelete.ClientId} is deleted");
                            db.Clients.Remove(clientToDelete);
                            db.SaveChanges();
                        }

                    }
                    else if (cmdArg[1] == "town")
                    {
                        Console.Write("Enter the town name who you want to delete: ");
                        string townToDelete = Console.ReadLine();
                        Town town = db.Towns.Where(x => x.Name == townToDelete).FirstOrDefault();
                        if (town != null)
                        {
                            db.Towns.Remove(town);
                            db.SaveChanges();
                            Console.WriteLine($"Succsessfully deleted the town with name {townToDelete}");
                        }
                        else
                        {
                            Console.WriteLine($"The town {townToDelete} not exist in database");
                        }
                    }

                }
                else if(cmdArg[0] == "show")
                {
                    if(cmdArg[1] == "clients")
                    {
                        Wait();
                        var allClients = db.Clients.OrderBy(x => x.FirstName).ToList();
                        var sb = new StringBuilder();
                        foreach (var c in allClients)
                        {
                            var curTown = db.Towns.FirstOrDefault(x => x.TownId == c.TownId);
                            sb.AppendLine($"{c.ClientId} - {c.FirstName} {c.MiddleName} {c.LastName} - {curTown.Name}");
                        }
                        
                        Console.WriteLine(sb);
                    }
                }

                else if (cmdArg[0] == "end")
                {
                    Console.WriteLine("Bye bye :)");
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong command. Please try again");
                }

            }
        }

        private static void PrintCommandHelpMessage()
        {
            var sb = new StringBuilder();
            sb.AppendLine("COMMAND LIST");
            sb.AppendLine();
            sb.AppendLine("ADD COMMANDS:");
            sb.AppendLine("-'Add Town'- Add new town in Towns table.");
            sb.AppendLine("-'Add Client'- Add new client to database.");
            sb.AppendLine();
            sb.AppendLine("DELETE COMMANDS");
            sb.AppendLine("-'Delete Town'- Delete town in Towns table if exist.");
            sb.AppendLine("-'Delete Client'- Delete client from database if exist.");
            sb.AppendLine();
            sb.AppendLine("SHOW COMMANDS");
            sb.AppendLine("-'SHOW CLIENTS'- Show list of all clients in database.");

            Console.WriteLine(sb.ToString());

        }

        private static void PrintWelcomeMessage()
        {
            DateTime currentDate = DateTime.UtcNow;
            var sb = new StringBuilder();
            sb.AppendLine("WELCOME TO GymSys");
            sb.AppendLine("------------------");
            sb.AppendLine($"The time is : {currentDate}");
            sb.AppendLine("------------------");
            sb.AppendLine($"Enter command for action or write HELP to see the command list");
            sb.AppendLine();
            Console.Write(sb.ToString());
        }

        private static void PrintLogo()
        {
            var sb = new StringBuilder();
            sb.AppendLine("            ___");
            sb.AppendLine("           (O o)");
            sb.AppendLine(@"          /  ^  \");
            sb.AppendLine(@"    ||   / /| |\ \   ||");
            sb.AppendLine(@"   =||==```=====```==||=");
            sb.AppendLine(@"    ||      | |      ||");
            sb.AppendLine(@"           /   \");
            sb.AppendLine(@"          _|   |_");
            Console.WriteLine(sb);
        }

        private static bool isNull(Object item)
        {
            return item == null;
        }

        private static void Wait()
        {
            Console.WriteLine("Loading... please wait");
        }
    }
}
