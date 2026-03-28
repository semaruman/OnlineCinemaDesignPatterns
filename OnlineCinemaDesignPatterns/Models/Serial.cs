using OnlineCinemaDesignPatternsConsole.Models.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinemaDesignPatternsConsole.Models
{
    public class Serial : IComparable<Serial>
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public SortedSet<User> Subscribers { get; set;  } = new SortedSet<User>();


        public void Notificate(INotification notification)
        {
            foreach (var subscriber in Subscribers)
            {
                subscriber.Mail.Add(notification);
            }
        }

        public override string ToString()
        {
            return $"{Id} - {Name}. {Subscribers.Count} подписчиков. \n\t{Description}";
        }

        public override bool Equals(object? obj)
        {
            return Id == ((Serial)obj).Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public int CompareTo(Serial? other)
        {
            if (other == null) return 1;
            return Id.CompareTo(other.Id);
        }
    }
}
