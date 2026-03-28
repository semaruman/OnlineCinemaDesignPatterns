using OnlineCinemaDesignPatternsConsole.Models.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinemaDesignPatternsConsole.Models
{
    public class User : IComparable<User>
    {

        public int Id { get; set; }
        public string FullName { get; set; }
        public List<INotification> Mail { get; set; } = new List<INotification>();

        public SortedSet<Serial> Serials { get; set; } = new SortedSet<Serial>();


        public void PrintMail()
        {

            Console.WriteLine($"Почта уведомлений пользователя {FullName}");

            foreach (var notification in Mail)
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.WriteLine(notification.Send());
            }
            Console.ResetColor();
        }

        public override string ToString()
        {
            return $"{Id} - {FullName}";
        }

        public override bool Equals(object? obj)
        {
            return Id == ((User)obj).Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public int CompareTo(User? other)
        {
            if (other == null) return 1;
            return Id.CompareTo(other.Id);
        }
    }
}
