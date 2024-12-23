﻿using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data
{
    public static class DataExtension
    {
        public static async Task  MigrateDbAsync(this WebApplication app) 
        {
            var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}
