using GameStore.Api.DTOs;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Mapping
{
    public static class  GameMapping
    {
        public static Game ToEntity(this CreateGameDto newGame)
        {
            Game game = new Game()
            {
                Name = newGame.Name,
                Price = newGame.Price,
                RelaseDate = newGame.RelaseDate,
                GenreId = newGame.GenreId,
            };
           return game;
        }
        public static Game ToEntity(this UpdateGameDto newGame,int id)
        {
            Game game = new Game()
            {
                Id = id,
                Name = newGame.Name,
                Price = newGame.Price,
                RelaseDate = newGame.RelaseDate,
                GenreId = newGame.GenreId,
            };
            return game;
        }

        public static GameSummaryDto ToSummaryDto(this Game game)
        {
            GameSummaryDto newGame = new
            (
                game.Id,
                game.Name,
                game.Genre!.Name,
                game.Price,
                game.RelaseDate
            );
            return newGame;
        }

        public static GameDetailedDto ToDetailedDto(this Game game)
        {
            GameDetailedDto newGame = new
            (
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.RelaseDate
            );
            return newGame;
        }
    }
}
