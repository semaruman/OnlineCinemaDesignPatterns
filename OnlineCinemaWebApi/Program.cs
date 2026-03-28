var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", IndexMethod);

app.Run();


IResult IndexMethod()
{
    string menu = @"
GET: /serials - получение списка сериалов
GET: /serials/id - получение информации о конкретном сериале
GET: /serials/id/subscribers - получение подписчиков сириала
";
    var data = new {
        message = menu
    };

    return Results.Ok(data);
}