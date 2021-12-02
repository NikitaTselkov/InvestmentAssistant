using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DataParserService.DataBase
{
    public static class PrepDb
    {
        private static IRepository repository;

        public static void InitDataBase(IApplicationBuilder app)
        {
            AppDbContext appDbContext;

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                appDbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();
                repository = new Repository(appDbContext);

                ApplyMigrate(appDbContext);

                InitSecurities();

                CalculateData();
            }
        }

        private static void CalculateData()
        {
            var lastUpdate = repository.GetLastUpdateCapitalizations();

            if (lastUpdate != null && lastUpdate.Value.Day > DateTime.Now.Day)
            {
                repository.DeleteAllCapitalizations();
                repository.CalculateCapitalizations();
                Console.WriteLine("--> Updated capitalizations");
            }
            else
            {
                repository.CalculateCapitalizations();
                Console.WriteLine("--> Calculated capitalizations");
            }
        }

        private static void InitSecurities()
        {
            if (!repository.IsSecuritiesTQBRContainsAny())
            {
                repository.InitSecuritiesTQBR();
                Console.WriteLine("--> Initialized Securities TQBR");
            }
            else
            {
                Console.WriteLine("--> Securities TQBR already exists");
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
