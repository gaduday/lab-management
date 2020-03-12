using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;
using Microsoft.VisualBasic.CompilerServices;

namespace LabManagement
{
    public class UserManagement
    {
        // Create a new user
        public static void CreateUser(IObjectContainer db)
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            var result = from User u in db select u;

            foreach (var item in result)
            {
                while (username == item.Username)
                {
                    Console.WriteLine("Invalid username!");
                    Console.Write("Enter username: ");
                    username = Console.ReadLine();
                }

                if (username != item.Username)
                {
                    Console.Write("Enter password: ");
                    string password = Console.ReadLine();

                    User user = new User(name, username, password);
                    db.Store(user);

                    Console.WriteLine("Stored {0}", user);
                    return;
                }
            }
        }

        public static void ListResult(IObjectSet result)
        {
            Console.WriteLine(result.Count);
            foreach (object item in result)
            {
                Console.WriteLine(item);
            }
        }

        // Display all users
        public static void RetrieveAllUsers(IObjectContainer db)
        {
            IObjectSet result = db.QueryByExample(typeof(User));
            ListResult(result);
        }

        // Update an user
        public static void UpdateAnUser(IObjectContainer db)
        {
            Console.Write("Enter username to edit: ");
            string username = Console.ReadLine();
            // linq query select user with username equal input username
            var result = from User u in db where u.Username == username select u;
            if (!result.Any())
            {
                Console.WriteLine("Invalid username. Please try again!");
            }

            foreach (var item in result)
            {
                string option;
                do
                {
                    Console.Write("Enter 1 to edit name or enter 2 to edit password: ");
                    option = Console.ReadLine();

                    if (option == "a")
                    {
                        Console.Write("New name: ");
                        string name = Console.ReadLine();
                        item.Name = name;
                    }
                    else if (option == "b")
                    {
                        Console.Write("New password: ");
                        string password = Console.ReadLine();
                        item.Password = password;
                    }
                    else
                    {
                        Console.WriteLine("Please select a valid option!");
                        option = Console.ReadLine();
                    }
                } while (option == "a" || option == "b");
            }
        }

        // public static void DeleteAnUser(IObjectContainer db)
        // {
        //     Console.Write("Enter username to edit: ");
        //     string username = Console.ReadLine();
        //     // linq query select user with username equal input username
        //     var result = from User u in db where u.Username == username select u;
        //     if (!result.Any())
        //     {
        //         Console.WriteLine("Invalid username. Please try again!");
        //     }
        //
        //     foreach (var item in result)
        //     {
        //         User user = new User();
        //         db.User.Remove(item);
        //     }
        // }
    }
}