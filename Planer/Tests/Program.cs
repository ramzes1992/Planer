﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model.Repositories;
using Model;

namespace Tests
{
    class Program
    {
        static UserRepository _userRepository = new UserRepository();

        static void Main(string[] args)
        {
            Console.WriteLine("Tests...");

            string input = string.Empty;

            while(input != "exit")
            {
                Console.WriteLine("write \"add\" to add new User or \"print\" to print all users");

                input = Console.ReadLine();

                switch (input)
                {
                    case "add":
                        Console.WriteLine("Name:");
                        var name = Console.ReadLine();
                        Console.WriteLine("Password:");
                        var pass = Console.ReadLine();
                        _userRepository.Add(new User() { Name = name, Password = pass });
                        break;
                    case "print":
                        PrintAllUsers();
                        break;
                    default:
                        break;
                }
            }


            Console.ReadKey();
        }

        static void PrintAllUsers()
        {
            Console.WriteLine("Printing All Users");
            foreach (var x in _userRepository.GetAll())
            {
                Console.WriteLine("ID: {0} name: {1} pass: {2}", x.Id, x.Name, x.Password);
            }
        }
    }
}
