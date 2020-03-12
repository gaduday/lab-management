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
            int id = int.Parse(Console.ReadLine());

            Computer computer = new Computer(id, false);
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
            int id;
            Console.Write("Please enter computer id: ");
            id = int.Parse(Console.ReadLine());
            var result = from Computer c in db where c.Id == id select c;
            var resultU = from User u in db where u.IsOnline select u;
            foreach (var item in result)
            {
                if (item.IsUsing)
                {
                    Console.WriteLine("Computer {0} is being used! Please select another computer!", item.Id);
                }
                else
                {
                    item.IsUsing = true;
                    foreach (var itemU in resultU)
                    {
                        item.UsingUsername = itemU.Username;
                    }
                }
            }
        }
    }
}