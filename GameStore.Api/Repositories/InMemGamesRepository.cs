using System.Collections;
using GameStore.Api.Entities;

namespace GameStore.Api.Repositories;

public class InMemGamesRepository
{
    private readonly List<Game> games = new()
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

    public IEnumerable<Game> GetAll() { return games; }

    public Game? Get(int id)
    {
        return games.Find(games => games.Id == id);
    }

    public void Create(Game game)
    {
        game.Id = games.Max(game => game.Id) + 1;
        games.Add(game);
    }

    public void Update(Game updatedGame)
    {
        var index = games.FindIndex(games => games.Id == updatedGame.Id);
        games[index] = updatedGame;
    }

    public void Delete(int id)
    {
        var index = games.FindIndex(games => games.Id == id);
        games.RemoveAt(index);
    }

}