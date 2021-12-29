using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentInvestmentAttractivenessService.DataBase
{
    public static class PrebDb
    {
        public static void InitDataBase(IApplicationBuilder app)
        {
            AppDbContext appDbContext;

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                appDbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();

                ApplyMigrate(appDbContext);
            }
        }

        private static void ApplyMigrate(AppDbContext context)
        {
            Console.WriteLine("--> Attempting to apply migrations...");
            try
            {
                context.Database.Migrate();

                Console.WriteLine($"--> Migrations were applied");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not run migratons: {ex.Message}");
            }
        }
    }
}
