using GameStore.Api.Data;
using GameStore.Api.DTOs;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GameStore.Api.EndPoints
{
    public static class GamesEndpoint
    {
        //private static readonly List<GameDto> games = new List<GameDto>
        //{
        ////new GameDto(
        //        1,
        //        "GTA V",
        //        "Arcade",
        //        10.99M,
        //        new DateTime(2010,6,15))

        //};
        private static bool isvalid(CreateGameDto game)
        {
            if (game.Name == null || game.GenreId == 0 || game.Name.Length > 50)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

            ////}
            public static  WebApplication MapGamesEndpoints(this WebApplication app)
        {

            //get Elements
            app.MapGet("/games", async(GameStoreContext dbContext) =>
            {
                var games = await dbContext.Games.Include(game => game.Genre).Select(game => game.ToSummaryDto()).AsNoTracking().ToListAsync();
                return Results.Ok(games);
            });
            
            //get Elements by id
            app.MapGet("/games/{id}",async (int id,GameStoreContext dbContext) =>
            {
                var game = await dbContext.Games.FindAsync(id);
                return game is not null ? Results.NotFound() : Results.Ok(game);
            }
            ).WithName("GetGame");

            app.MapPost("/games",async (CreateGameDto newGame,GameStoreContext dbContext) =>
            {

                Game game = newGame.ToEntity();
                
              
                dbContext.Games.Add(game);
                await dbContext.SaveChangesAsync();
                return  Results.CreatedAtRoute("GetGame", new { id = game.Id }, game.ToDetailedDto());
            }) ;

            app.MapPut("/games/{id}",async (int id, UpdateGameDto updateGame,GameStoreContext dbContext) =>
            {
                var existingGame =await  dbContext.Games.FindAsync(id);
                if (existingGame == null)
                {
                    return Results.NotFound();
                }
                dbContext.Entry(existingGame).CurrentValues.SetValues(updateGame.ToEntity(id));

                await dbContext.SaveChangesAsync();
                
                return Results.NoContent();
            }
            );

            app.MapDelete("/games/{id}",async (int id,GameStoreContext dbContext) =>
            {
                var existingGame = await dbContext.Games.FindAsync(id);
                if (existingGame == null)
                {
                    return Results.NotFound();
                }
                dbContext.Games.Remove(existingGame);
                await dbContext.SaveChangesAsync();
                
                return Results.NoContent();
            }
            );

            return app;
        }
    }

}
