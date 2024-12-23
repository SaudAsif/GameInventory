using GameStore.Api.Data;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.EndPoints
{
    public static class GenreEndpoints
    {
        public static WebApplication MapGenreEndpoints(this WebApplication app)
        {
            app.MapGet("/genre",async (GameStoreContext dbContext) =>
            {
                var genres = await dbContext.Genres.Select(genre => genre.ToDto()).AsNoTracking().ToListAsync();
                return Results.Ok(genres);
            }
            );

            return app;
        }
    }
}
