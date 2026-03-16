using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinemaDesignPatterns.Models.Notifications
{
    public interface INotification
    {
        INotification Parent { get; set; }

        string Text { get; set; }

        public string Send();
    }
}
