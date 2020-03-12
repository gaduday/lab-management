using System;
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

            Computer computer = new Computer(int.Parse(id), false);
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
            
            while (true)
            {
                if (!int.TryParse(id, out num)) {
                    Console.WriteLine("ID must be number! Please try again!");
                    Console.Write("Please enter computer id: ");
                    id = Console.ReadLine();
                }
                else
                {
                    var result = from Computer c in db where c.Id.ToString() == id select c;
                    var resultU = from User u in db where u.IsOnline select u;
                    foreach (var item in result)
                    {
                        if (item.IsUsing)
                        {
                            Console.WriteLine("Computer {0} is being used! Please select another computer!", item.Id);
                        }
                        else
                        {
                            Console.WriteLine("Computer is starting...");
                            item.IsUsing = true;
                            foreach (var itemU in resultU)
                            {
                                item.UsingUsername = itemU.Username;
                            }
                        }
                    }
                    return;
                }
            }
        }
    }
}