using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model.Repositories;
using Model;
using System.Security;
using System.Security.Cryptography;

namespace Tests
{
    class Program
    {
        static UserRepository _userRepository = new UserRepository();

        static void Main(string[] args)
        {            
            

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
