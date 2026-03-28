using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OnlineCinemaDesignPatternsConsole.Models;
using OnlineCinemaDesignPatternsConsole.Models.Notifications;

var serials = new SortedSet<Serial>();

serials.Add(new Serial {Id = 1, Name = "Смешарики", Description = "Самый крутой сериал"});
serials.Add(new Serial { Id = 2, Name = "Наруто", Description = "описание" });
serials.Add(new Serial { Id = 3, Name = "Шерлок", Description = "детектив" });

var users = new SortedSet<User>();
users.Add(new User { Id = 1, FullName = "Евлампий" });
users.Add(new User { Id = 2, FullName = "Архиоп" });

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", IndexMethod);
app.MapGet("/serials", GetSerials);
app.MapGet("/serials/{id:int}", GetSerialInfo);
app.MapGet("/serials/{id:int}/subscribers", GetSerialSubscribers);
app.MapGet("/users", GetUsers);
app.MapGet("/users/{id:int}", GetUserInfo);
app.MapGet("/users/{id:int}/serials", GetUserSerials);
app.MapGet("/users/{id:int}/notifications", GetUserNotifications);
app.MapPost("/users/{id:int}/subscribe/{serialId:int}", SubscribeUser);
app.MapPost("/users/{id:int}/unsubscribe/{serialId:int}", UnsubscribeUser);
app.MapPost("/users/add", AddUser);
app.MapPost("/serials/{serialId:int}/addnotification", AddNotification);
app.Run();


IResult IndexMethod()
{
    string menu = @"
GET: /serials - получение списка сериалов
GET: /serials/id - получение информации о конкретном сериале
GET: /serials/id/subscribers - получение подписчиков сириала
GET: /users - получение всех пользователей
GET: /users/id - получение информации о конкретном пользователе
GET: /users/id/serials - получение сериалов пользователя
GET: /users/id/notifications - получение уведомлений пользователя
POST: /users/id/subscribe/serialId - пидписаться на сериал
POST: /users/id/unsubscribe/serialId - отписаться от сериала
POST: /users/add - добавление пользователя
POST: /serials/id/addnotification - добавление уведомления для конкретного сериала
";
    var data = new
    {
        message = menu
    };

    return Results.Ok(data);
}

IResult GetSerials()
{
    return Results.Ok(serials.Select(s => s.ToString()));
}

IResult GetSerialInfo(int id)
{
    var serial = serials.FirstOrDefault(x => x.Id == id);

    if (serial == null)
    {
        return Results.BadRequest(new { message = "Сериал не найден" });
    }
    else
    {
        return Results.Ok(new { message = serial.ToString() });
    }
}

IResult GetSerialSubscribers(int id)
{
    var serial = serials.FirstOrDefault(x => x.Id == id);

    if (serial == null)
    {
        return Results.BadRequest(new { message = "Сериал не найден" });
    }
    else
    {
        return Results.Ok(serial.Subscribers);
    }
}

IResult GetUsers()
{
    return Results.Ok(users.Select(u => u.ToString()));
}

IResult GetUserInfo(int id)
{
    var user = users.FirstOrDefault(x => x.Id == id);
    if (user == null)
    {
        return Results.BadRequest(new { message = "Пользователь не найден" });
    }
    else
    {
        return Results.Ok(new { message = user.ToString() });
    }
}

IResult GetUserSerials(int id)
{
    var user = users.FirstOrDefault(x => x.Id == id);

    if (user == null)
    {
        return Results.BadRequest(new { message = "Пользователь не найден" });
    }
    else
    {
        return Results.Ok(user.Serials);
    }
}

IResult GetUserNotifications(int id)
{
    var user = users.FirstOrDefault(x => x.Id == id);

    if (user == null)
    {
        return Results.BadRequest(new { message = "Пользователь не найден" });
    }
    else
    {
        return Results.Ok(user.Mail.Select(n => n.Send()));
    }
}

IResult SubscribeUser(int id, int serialId)
{
    var user = users.FirstOrDefault(x => x.Id == id);
    var serial = serials.FirstOrDefault(x => x.Id == id);

    if (user == null)
    {
        return Results.BadRequest(new { message = "Пользователь не найден" });
    }
    else if (serial == null)
    {
        return Results.BadRequest(new { message = "Сериал не найден" });
    }
    else
    {
        serial.Subscribers.Add(user);
        user.Serials.Add(serial);
        return Results.Ok(new {message = $"Пользователь {user.FullName} подписался на сериал {serial.Name}"});
    }
}

IResult UnsubscribeUser(int id, int serialId)
{
    var user = users.FirstOrDefault(x => x.Id == id);
    var serial = serials.FirstOrDefault(x => x.Id == id);

    if (user == null)
    {
        return Results.BadRequest(new { message = "Пользователь не найден" });
    }
    else if (serial == null)
    {
        return Results.BadRequest(new { message = "Сериал не найден" });
    }
    else
    {
        serial.Subscribers.Remove(user);
        user.Serials.Remove(serial);
        return Results.Ok(new { message = $"Пользователь {user.FullName} отписался от сериала {serial.Name}" });
    }
}

IResult AddUser([FromBody] User user)
{
    if (user == null || string.IsNullOrEmpty(user.FullName))
    {
        return Results.BadRequest(new { Error = "Ошибка данных пользователя" });
    }
    else
    {
        users.Add(user);
        return Results.Ok(new { message = $"Пользователь {user.FullName} добавлен" });
    }
}

IResult AddNotification(
    int serialId, 
    [FromBody] AddNotificationRequest bodyRequest
    )
{
    var serial = serials.FirstOrDefault(x => x.Id == serialId);

    if (serial == null)
    {
        return Results.BadRequest(new { message = "Сериал не найден" });
    }
    string text = bodyRequest.Text;
    INotification notification = null;
    foreach (string type in bodyRequest.NotificationTypes)
    {
        if (type == "advert")
        {
            notification = new AdvertDecorator(text, notification);
        }
        else if (type == "email")
        {
            notification = new EmailDecorator(text, notification);

        }
        else if (type == "sale")
        {
            notification = new SaleDecorator(text, notification);

        }
        else if (type == "sms")
        {
            notification = new SMSDecorator(text, notification);

        }
        else if (type == "trailer")
        {
            notification = new TrailerDecorator(text, notification);
        }
        else
        {
            return Results.BadRequest(new { message = $"Тип уведомления '{type}' отсутствует" });
        }
    }

    if (notification == null)
    {
        return Results.BadRequest(new { message = "Уведомление отсутствует" });
    }

    serial.Notificate(notification);
    return Results.Ok(new {message = "Уведомление создано успешно"});
}


public class AddNotificationRequest
{
    public List<string> NotificationTypes { get; set; } = new List<string>();
    public string Text { get; set; } = string.Empty;
}