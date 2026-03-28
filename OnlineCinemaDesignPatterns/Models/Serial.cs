using OnlineCinemaDesignPatternsConsole.Models.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinemaDesignPatternsConsole.Models
{
    public class Serial
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<User> Subscribers { get; set;  } = new List<User>();

        //навигационное свойство
        public Cinema Cinema { get; set; }

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
    }
}
