using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model.Repositories;
using Model;
using System.Security;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;

using Universal;

namespace Tests
{
    public static class Program
    {
        static UserRepository _userRepository = new UserRepository();

        static void Main(string[] args)
        {
            Stopwatch sp = new Stopwatch();

            sp.Start();

            for (int i = 0; i < int.MaxValue; i++)
            {
                calc(i);
            }
            sp.Stop();

            Console.WriteLine("Sync Complete! time: {0}", sp.Elapsed.ToString());


            sp.Restart();
            Parallel.For(0, int.MaxValue, (i) =>
            {
                calc(i);
            });
            sp.Stop();

            Console.WriteLine("Async Complete! time: {0}", sp.Elapsed.ToString());

            //10.Times((i) => { Console.WriteLine("some text: {0}", i); });

            Console.ReadKey();

            //Console.WriteLine("Podaj Imię:");
            //var name = Console.ReadLine();

            //Console.WriteLine("Cześć " + name);

            //Console.WriteLine("Podaj rok urodzenia:");
            //var inputYear = Console.ReadLine();

            //Console.WriteLine("Masz {0} lat", DateTime.Now.Year - int.Parse(inputYear));

            //Console.ReadKey();
        }

        static void calc(int i)
        {
            var x = Math.Cos(i + Math.Log(i)) * Math.Asin(i + Math.Log(i));
        }

        static void PrintAllUsers()
        {
            Console.WriteLine("Printing All Users");
            foreach (var x in _userRepository.GetAll())
            {
                Console.WriteLine("ID: {0} name: {1} pass: {2}", x.Id, x.Name, x.Password);
            }
        }
        public static void Times(this int count, Action action)
        {
            for (int i = 0; i < count; i++)
            {
                action();
            }
        }
    }
}
