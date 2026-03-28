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

app.Run();


IResult IndexMethod()
{
    string menu = @"
GET: /serials - получение списка сериалов
GET: /serials/id - получение информации о конкретном сериале
GET: /serials/id/subscribers - получение подписчиков сириала
";
    var data = new
    {
        message = menu
    };

    return Results.Ok(data);
}

IResult GetSerials()
{
    return Results.Ok(serials);
}


