using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinemaDesignPatterns.Models.Notifications
{
    public class SaleDecorator : INotification
    {
        public INotification Parent { get; set; }
        public string Text { get; set; }

        public SaleDecorator(string text, INotification parent = null)
        {
            Text = text;
            Parent = parent;
        }

        public string Send()
        {
            StringBuilder res = new StringBuilder($"[Скидка] {Text}\n");

            if (Parent != null)
            {
                res.Append(Parent.Send());
            }

            return res.ToString();
        }
    }
}
