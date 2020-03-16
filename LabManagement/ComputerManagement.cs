using System;
using System.ComponentModel;
using System.Linq;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;

namespace LabManagement
{
    public class ComputerManagement
    {
        // Create a new computer
        public static void AddNewComputer(IObjectContainer db)
        {
            Console.Write("Enter id: ");
            string id = Console.ReadLine();
            int num;

            while (!int.TryParse(id, out num))
            {
                Console.WriteLine("ID must be number! Please try again!");
                Console.Write("Enter id: ");
                id = Console.ReadLine();
            }

            var result = from Computer c in db select c;

            foreach (var item in result)
            {
                while (int.Parse(id) == item.Id)
                {
                    Console.WriteLine("Computer existed!");
                    Console.Write("Enter id: ");
                    id = Console.ReadLine();
                }
            }

            Computer computer = new Computer(int.Parse(id), false, null);
            Console.WriteLine("Added a new computer with id = {0}", id);
            db.Store(computer);

            Console.WriteLine("Stored {0}", computer);
        }

        //Display all computer (contain status)
        public static void DisplayAllComputers(IObjectContainer db)
        {
            IObjectSet result = db.QueryByExample(typeof(Computer));
            UserManagement.ListResult(result);
        }

        // Use a computer
        public static void UseAComputer(IObjectContainer db)
        {
            Console.Write("Please enter computer id: ");
            string id = Console.ReadLine();
            int num;

            while (!int.TryParse(id, out num))
            {
                Console.WriteLine("ID must be number! Please try again!");
                Console.Write("Please enter computer id: ");
                id = Console.ReadLine();
            }

            var result = from Computer c in db where c.Id.ToString() == id select c;

            foreach (var item in result)
            {
                if (item.IsUsing)
                {
                    Console.WriteLine("Computer {0} is being used! Please select another computer!", item.Id);
                }
                else
                {
                    var resultU = from User u in db where u.IsOnline select u;
                    item.IsUsing = true;

                    string timeUsing = DateTime.Now.ToString("h:mm:ss tt");
                    UsageInformation ui = new UsageInformation(timeUsing, null, item.Id, Program.userUsing);
                    db.Store(ui);

                    string userUsing = "";
                    Console.WriteLine("Computer {0} is starting at {1} by {2}!", id, timeUsing, Program.userUsing);
                }
            }
        }

        public static void StopUsingAComputer(IObjectContainer db)
        {
            Console.Write("To stop using, please enter computer id: ");
            string id = Console.ReadLine();
            int num;

            while (!int.TryParse(id, out num))
            {
                Console.WriteLine("ID must be number! Please try again!");
                Console.Write("Please enter computer id: ");
                id = Console.ReadLine();
            }

            var result = from Computer c in db where c.Id.ToString() == id select c;
            var resultUI = from UsageInformation ui in db select ui;

            foreach (var item in result)
            {
                foreach (var itemUI in resultUI)
                {
                    if (item.IsUsing && itemUI.ComputerId.ToString() == id && itemUI.UserUsername == Program.userUsing)
                    {
                        item.IsUsing = false;

                        string timeUsing = DateTime.Now.ToString("h:mm:ss tt");
                        UsageInformation ui = new UsageInformation(null, timeUsing, item.Id, Program.userUsing);
                        db.Store(ui);

                        string userUsing = "";
                        Console.WriteLine("Computer {0} is shut down at {1} by {2}!", id, timeUsing, Program.userUsing);
                        return;
                    }
                }

                Console.WriteLine("You can't stop this computer!");
            }
        }

        public static void UpdateAComputer(IObjectContainer db)
        {
            Console.Write("Enter username to edit: ");
            string id = Console.ReadLine();
            int num;

            while (!int.TryParse(id, out num))
            {
                Console.WriteLine("ID must be number! Please try again!");
                Console.Write("Please enter computer id: ");
                id = Console.ReadLine();
            }

            var result = from Computer c in db where c.Id.ToString() == id select c;

            if (!result.Any())
            {
                Console.WriteLine("Invalid Id. Please try again!");
            }

            foreach (var item in result)
            {
                string option;
                Console.Write("Enter 1 to edit name or enter 2 to edit password: ");

                string newId = Console.ReadLine();
                int newNum;

                while (!int.TryParse(newId, out newNum))
                {
                    Console.WriteLine("ID must be number! Please try again!");
                    Console.Write("Please enter computer id: ");
                    newId = Console.ReadLine();

                    item.Id = int.Parse(newId);
                }
            }
        }
        
        public static void DeleteAComputer(IObjectContainer db)
        {
            string id = Console.ReadLine();
            int num;

            while (!int.TryParse(id, out num))
            {
                Console.WriteLine("ID must be number! Please try again!");
                Console.Write("Please enter computer id: ");
                id = Console.ReadLine();
            }
            // linq query select user with username equal input username
            var result = from Computer c in db where c.Id.ToString() == id select c;
            if (!result.Any())
            {
                Console.WriteLine("Invalid username. Please try again!");
            }
        
            foreach (var item in result)
            {
                db.Delete(item);
            }
        }
        
    }
}