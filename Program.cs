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
                Console.WriteLine();

                if (command == "help")
                {
                    PrintCommandHelpMessage();
                   // return;
                }

                else if(command == "addtown")
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
                            break;
                        }
                        else if(command == "n")
                        {
                            Console.WriteLine("Is canceled. Please try again.");
                        }
                    }

                    if(isCommandFinished)
                    {
                        break;
                    }
                }

                else if(command == "deletetown")
                {
                    Console.Write("Enter the town name who you want to delete: ");
                    string townToDelete = Console.ReadLine();
                    Town town = db.Towns.Where(x => x.Name == townToDelete).FirstOrDefault();
                    if(town != null)
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
        }

        private static void PrintCommandHelpMessage()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Possible commands are:");
            sb.AppendLine();
            sb.AppendLine("-'AddTown'- Add new town in Towns table.");
            sb.AppendLine("-'DeleteTown'- Delete town in Towns table if exist.");

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
    }
}
