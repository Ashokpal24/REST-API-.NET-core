using GameStore.Api.Entities;

const string GetGameEndpointName = "GetGame";

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

}).WithName(GetGameEndpointName);

app.MapPost("/games", (Game game) =>
{
    game.Id = games.Max(game => game.Id) + 1;
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});


app.MapPut("/games/{id}", (int id, Game updateGame) =>
{
    Game? existingGame = games.Find(game => game.Id == id);
    if (existingGame is null)
    {
        return Results.NotFound();
    }

    existingGame.Name = updateGame.Name;
    existingGame.Genre = updateGame.Genre;
    existingGame.Price = updateGame.Price;
    existingGame.ReleaseDate = updateGame.ReleaseDate;
    existingGame.ImageUri = updateGame.ImageUri;

    return Results.NoContent();

});

app.MapDelete("/games/{id}", (int id) =>
{
    Game? delGame = games.Find(game => game.Id == id);
    if (delGame is not null)
    {
        games.Remove(delGame);
    }
    return Results.NoContent();
});
app.Run();
