using GameStore.Api.Entities;
using GameStore.Api.Repositories;

namespace GameStore.Api.Endpoints;
public static class GameEndpoints
{
    const string GetGameEndpointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {
        InMemGamesRepository repository = new();

        var group = routes.MapGroup("/games").WithParameterValidation();

        routes.MapGet("", () => "Hi There!");

        group.MapGet("/", () => repository.GetAll());

        group.MapGet("/{id}", (int id) =>
        {
            Game? game = repository.Get(id);

            return game is not null ? Results.Ok(game) : Results.NotFound();

        }).WithName(GetGameEndpointName);

        group.MapPost("/", (Game game) =>
        {
            repository.Create(game);
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });


        group.MapPut("/{id}", (int id, Game updateGame) =>
        {
            Game? existingGame = repository.Get(id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }

            existingGame.Name = updateGame.Name;
            existingGame.Genre = updateGame.Genre;
            existingGame.Price = updateGame.Price;
            existingGame.ReleaseDate = updateGame.ReleaseDate;
            existingGame.ImageUri = updateGame.ImageUri;

            repository.Update(existingGame);
            return Results.NoContent();

        });

        group.MapDelete("/{id}", (int id) =>
        {
            Game? delGame = repository.Get(id);
            if (delGame is not null)
            {
                repository.Delete(id);
            }
            return Results.NoContent();
        });

        return group;
    }

}