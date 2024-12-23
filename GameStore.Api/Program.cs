using GameStore.Api.Data;
using GameStore.Api.EndPoints;
var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Use CORS
app.UseCors("AllowAll");
app.MapGamesEndpoints();
app.MapGenreEndpoints();
 
await  app.MigrateDbAsync();



app.Run();
