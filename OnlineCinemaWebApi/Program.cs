using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OnlineCinemaDesignPatternsConsole.Models;

var serials = new List<Serial>{
            new Serial {Id = 1, Name = "Смешарики", Description = "Самый крутой сериал"},
            new Serial {Id = 2, Name = "Наруто", Description = "описание"},
            new Serial {Id = 3, Name = "Шерлок", Description = "детектив"}
        };
var users = new List<User>{
            new User {Id = 1, FullName = "Евлампий"},
            new User {Id = 2, FullName = "Архиоп"}
        };

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
app.MapGet("/users/add", AddUser);

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
GET: /users/add - добавление пользователя
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