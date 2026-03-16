using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinemaDesignPatterns.Models
{
    public class Cinema
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Serial> Serials = new List<Serial>();
        public List<User> Users = new List<User>();

        public void PrintSerialList()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"Добро пожаловать в кинотеатр {Name}!");

            Console.BackgroundColor = ConsoleColor.Magenta;

            foreach (var serial in Serials )
            {
                Console.WriteLine(serial);
            }

            Console.ResetColor();
        }

        public void PrintUsers()
        {

            Console.BackgroundColor = ConsoleColor.DarkYellow;
            foreach (var user in Users)
            {
                Console.WriteLine(user);
            }
            Console.ResetColor();
        }
    }
}
