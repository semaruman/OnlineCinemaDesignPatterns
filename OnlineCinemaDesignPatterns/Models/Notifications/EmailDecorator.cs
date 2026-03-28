using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinemaDesignPatternsConsole.Models.Notifications
{
    public class EmailDecorator : INotification
    {
        public INotification Parent { get; set; }
        public string Text { get; set; }

        public EmailDecorator(string text, INotification parent = null)
        {
            Text = text;
            Parent = parent;
        }

        public string Send()
        {
            StringBuilder res = new StringBuilder($"[Email] {Text}\n");

            if (Parent != null)
            {
                res.Append(Parent.Send());
            }

            return res.ToString();
        }
    }
}
