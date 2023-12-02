
using GameStore.Api.Entities;

List<Game> games = new()
{
    new Game()
    {
        Id=1,
        Name="Assassin Cred II",
        Genre="Action/RPG",
        Price=19.99M,
        ReleaseDate=new DateTime(2008,11,23),
        ImageUri="https://placehold.co/100"
    },
    new Game()
    {
        Id=2,
        Name="God of War II Chains of olympus",
        Genre="Action",
        Price=29.99M,
        ReleaseDate=new DateTime(2005,7,16),
        ImageUri="https://placehold.co/100"
    },
    new Game()
    {
        Id=3,
        Name="Metal Gear Solid V phatom pain",
        Genre="Action/RPG",
        Price=39.99M,
        ReleaseDate=new DateTime(2015,3,9),
        ImageUri="https://placehold.co/100"
    }
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/games", () => games);
app.MapGet("/", () => "Hi There!");

app.MapGet("/games/{id}", (int id) =>
{
    Game? game = games.Find(game => game.Id == id);

    if (game is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(game);

});

app.Run();
