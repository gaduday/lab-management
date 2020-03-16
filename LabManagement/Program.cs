using System;
using System.IO;
using Db4objects.Db4o;
using Db4objects.Db4o.CS;
using Db4objects.Db4o.CS.Config;
using Db4objects.Db4o.Linq;

namespace LabManagement
{
    public class Program
    {
        public static string userUsing;
        
        static readonly string YapFileName = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "lab-management.yap");

        // private static readonly string YapFileName = "/home/gagaduday/RiderProjects/LabManagement/lab-management.yap";

        private static IObjectContainer db = Db4oEmbedded.OpenFile(YapFileName);
        
        static void Main(string[] args)
        {
            string option;
            do
            {
                Console.WriteLine("a. Sign up");
                Console.WriteLine("b. Login");

                Console.Write("Choose an option: ");
                option = Console.ReadLine();

                switch (option)
                {
                    case "a":
                        UserManagement.CreateUser(db);
                        break;
                    case "b":
                        Login();
                        break;
                    default:
                        Console.WriteLine("Please choose a valid option!");
                        break;
                }
            } while (option != "a" || option != "b");
        }

        private static void Login()
        {
            int loginCounter = 0;

            while (loginCounter < 3)
            {
                Console.Write("Username: ");
                string username = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                
                var result = from User u in db select u;
            
                foreach (var item in result)
                {
                    if (item.Username == username && item.Password == password)
                    {
                        userUsing = item.Username;
                        item.IsOnline = true;
                        Console.WriteLine("Login successful!");
                        if (item.Username == "admin")
                        {
                            ShowMenuAdmin();
                        }
                        else
                        {
                            ShowMenuUser();
                        }
                        
                        return;
                    }
                }
                loginCounter++;
                Console.WriteLine("Invalid username or password! Please login again!");
                Console.WriteLine(3 - loginCounter + "login attempts remaining!");
            }
        }

        private static void ShowMenuAdmin()
        {
            string option = "";
            do
            {
                Console.WriteLine("a. Add a new user");
                Console.WriteLine("b. Display all users");
                Console.WriteLine("c. Update a new user");
                Console.WriteLine("d. Delete a new user");
                Console.WriteLine("e. Add a new computer");
                Console.WriteLine("f. Display all computers");
                Console.WriteLine("g. Update a new computer");
                Console.WriteLine("h. Delete a new computer");
                Console.WriteLine("i. Use a computer");
                Console.WriteLine("j. Show usage history");
                Console.WriteLine("k. Exit");

                Console.Write("Choose an option: ");
                option = Console.ReadLine();

                switch (option)
                {
                    case "a":
                        UserManagement.CreateUser(db);
                        break;
                    case "b":
                        UserManagement.RetrieveAllUsers(db);
                        break;
                    case "c":
                        UserManagement.UpdateAnUser(db);
                        break;
                    case "d":
                        UserManagement.DeleteAnUser(db);
                        break;
                    case "e":
                        ComputerManagement.AddNewComputer(db);
                        break;
                    case "f":
                        ShowUserAndComputer(db);
                        break;
                    case "g":
                        ComputerManagement.UpdateAComputer(db);
                        break;
                    case "h":
                        ComputerManagement.DeleteAComputer(db);
                        break;
                    case "i":
                        ComputerManagement.UseAComputer(db);
                        break;
                    case "j":
                        ShowUsageHistory(db);
                        break;
                    case "k":
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Please choose a valid option!");
                        break;
                }
            } while (option != "k");
        }

        private static void ShowMenuUser()
        {
            string option = "";
            do
            {
                Console.WriteLine("a. Use a computer");
                Console.WriteLine("b. Display all computers");
                Console.WriteLine("c. Stop using a computer");
                Console.WriteLine("d. Logout");
                Console.WriteLine("e. Exit");

                Console.Write("Choose an option: ");
                option = Console.ReadLine();

                switch (option)
                {
                    case "a":
                        ComputerManagement.UseAComputer(db);
                        break;
                    case "b":
                        ComputerManagement.DisplayAllComputers(db);
                        break;
                    case "c":
                        ComputerManagement.StopUsingAComputer(db);
                        break;;
                    case "d":
                        // logout user
                        break;
                    case "e":
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Please choose a valid option!");
                        break;
                }
            } while (option != "e");
        }

        private static void ShowUserAndComputer(IObjectContainer db)
        {
            var result = from UsageInformation ui in db select ui;

            foreach (var item in result)
            {
                Console.WriteLine("Computer {0} is using by {1}", item.ComputerId, item.UserUsername);
            }
        }

        private static void ShowUsageHistory(IObjectContainer db)
        {
            Console.Write("Please enter computer id to show usage history: ");
            string id = Console.ReadLine();
            int num;
            
            while (!int.TryParse(id, out num))
            {
                Console.WriteLine("ID must be number! Please try again!");
                Console.Write("Please enter computer id: ");
                id = Console.ReadLine();
            }
            
            var result = from UsageInformation ui in db where ui.ComputerId.ToString() == id select ui;

            foreach (var item in result)
            {
                Console.WriteLine("Computer {0} is used by {1} at {2} and ended at {3}", item.ComputerId, item.UserUsername, item.TimeStartUsing, item. TimeFinishUsing);
            }
        }
    }
}