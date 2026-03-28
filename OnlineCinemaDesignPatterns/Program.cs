using OnlineCinemaDesignPatternsConsole.Models;
using OnlineCinemaDesignPatternsConsole.Models.Notifications;

public class Program
{
    public static Cinema Cinema { get; } = new Cinema
    {
        Id = 1,
        Name = "Волжский",
        Serials =
        {
            new Serial {Id = 1, Name = "Смешарики", Description = "Самый крутой сериал"},
            new Serial {Id = 2, Name = "Наруто", Description = "описание"},
            new Serial {Id = 3, Name = "Шерлок", Description = "детектив"}
        },
        Users =
        {
            new User {Id = 1, FullName = "Евлампий"},
            new User {Id = 2, FullName = "Архиоп"}
        }
    };
    public static void Main(string[] args)
    {
        

        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ResetColor();

        string menuValue;

        while (true)
        {
            Console.WriteLine("Выберите роль");
            Console.WriteLine("1. Администратор");
            Console.WriteLine("2. Пользователь");
            Console.WriteLine("0. Выход");
            menuValue = Console.ReadLine();

            if (menuValue == "0")
            {
                Console.WriteLine("До свидания!");
                return;
            }
            else if (menuValue == "1")
            {
                AdminMenu();
            }
            else if (menuValue == "2")
            {
                Cinema.PrintUsers();

                Console.WriteLine("Введите ID пользователя");

                int id = Convert.ToInt32(Console.ReadLine());

                var user = Cinema.Users.FirstOrDefault(u => u.Id == id);

                if (user == null)
                {
                    Console.WriteLine("Такого пользователя не существует!");
                    continue;
                }
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine($"Добро пожаловать, {user.FullName}");
                Console.ResetColor();

                UserMenu(user);
            }

            Console.Write("Введите для продолжения...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    public static void UserMenu(User user)
    {
        string menuValue;

        while (true)
        {
            Console.WriteLine($"Пользователь: {user.FullName}(ID: {user.Id})");
            Console.WriteLine("1 - Посмотреть мои подписки");
            Console.WriteLine("2 - Посмотреть список сериалов");
            Console.WriteLine("3 - Подписаться на сериал");
            Console.WriteLine("4 - Отписаться от сериала");
            Console.WriteLine("5 - Посмотреть почтовый ящик");
            Console.WriteLine("0 - выйти из профиля");
            menuValue = Console.ReadLine();

            if (menuValue == "0")
            {
                Console.WriteLine($"{user.FullName}, Вы вышли из профиля");
                return;
            }
            else if (menuValue == "1")
            {
                foreach (var serial in user.Serials)
                {
                    Console.WriteLine(serial);
                }
            }
            else if (menuValue == "2")
            {
                foreach(var serial in Cinema.Serials)
                {
                    Console.WriteLine(serial);
                }
            }
            else if (menuValue == "3")
            {
                Console.WriteLine("Введите ID сериала");
                int serialId = Convert.ToInt32(Console.ReadLine());

                var serial = Cinema.Serials.FirstOrDefault(s => s.Id == serialId);

                serial.Subscribers.Add(user);
                user.Serials.Add(serial);

                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine($"{user.FullName}, Вы подписались на сериал {serial.Name}!");
                Console.ResetColor();
            }
            else if (menuValue == "4")
            {
                Console.WriteLine("Введите ID сериала");
                int serialId = Convert.ToInt32(Console.ReadLine());
                var serial = Cinema.Serials.FirstOrDefault(s => s.Id == serialId);

                serial.Subscribers.Remove(user);

                user.Serials.Remove(serial);

                Console.WriteLine($"{user.FullName}, Вы отписались от сериала {serial.Name}!");

            }
            else if (menuValue == "5")
            {
                user.PrintMail();
            }

            Console.Write("Введите для продолжения...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    public static void AdminMenu()
    {
        Console.WriteLine("Выберите сериал для уведомлений");
        foreach (var serial in Cinema.Serials)
        {
            Console.WriteLine(serial);
        }

        int serialId = Convert.ToInt32(Console.ReadLine());
        var selectedSerial = Cinema.Serials.FirstOrDefault(s => s.Id == serialId);

        var notif = CreateCombineNotification(selectedSerial);

        selectedSerial.Notificate(notif);
    }

    public static INotification CreateCombineNotification(Serial serial)
    {
        INotification notification = null;

        while (true)
        {
            Console.WriteLine("Какое уведомление добавить?");
            Console.WriteLine("1 - Реклама");
            Console.WriteLine("2 - Email");
            Console.WriteLine("3 - Скидка");
            Console.WriteLine("4 - СМС");
            Console.WriteLine("5 - Трейлер");
            Console.WriteLine("0 - Выход");

            string changeNotification = Console.ReadLine();

            if (changeNotification == "0")
            {
                return notification;
            }
            else if (changeNotification == "1")
            {
                Console.WriteLine("Введите текст уведомления");
                string text = Console.ReadLine();

                notification = new AdvertDecorator(text, notification);

                
            }
            else if (changeNotification == "2")
            {
                Console.WriteLine("Введите текст уведомления");
                string text = Console.ReadLine();

                notification = new EmailDecorator(text, notification);

            }
            else if (changeNotification == "3")
            {
                Console.WriteLine("Введите текст уведомления");
                string text = Console.ReadLine();

                notification = new SaleDecorator(text, notification);

            }
            else if (changeNotification == "4")
            {
                Console.WriteLine("Введите текст уведомления");
                string text = Console.ReadLine();

                notification = new SMSDecorator(text, notification);

            }
            else if (changeNotification == "5")
            {
                Console.WriteLine("Введите текст уведомления");
                string text = Console.ReadLine();

                notification = new TrailerDecorator(text, notification);

            }
        }
    }
}