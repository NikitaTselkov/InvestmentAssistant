using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DataParserService.Data
{
    public static class PrepDb
    {
        public static void InitDataBase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                ApplyMigrate(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void ApplyMigrate(AppDbContext context)
        {
            Console.WriteLine("--> Attempting to apply migrations...");
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not run migratons: {ex.Message}");
            }        
        }
    }
}
