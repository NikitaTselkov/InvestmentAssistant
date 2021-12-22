using DataParserService.DataParser;
using DataParserService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DataParserService.DataBase
{
    public static class PrepDb
    {
        private static IRepository _repository;

        public static void InitDataBase(IApplicationBuilder app)
        {
            AppDbContext appDbContext;

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                appDbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();
                _repository = new Repository(appDbContext);

                ApplyMigrate(appDbContext);
                InitSecurities();
                InitCompanies();
            }
        }

        private static void InitCompanies()
        {
            var securitieTQBRCode = ((int)SecuritiesTQBR.Stock).ToString();

            foreach (var securitieTQBR in _repository.GetSecuritiesTQBR().Where(w => w.SECTYPE == securitieTQBRCode))
            {
                if (!_repository.IsCompanyExists(securitieTQBR))
                {
                    _repository.AddCompany(new Company(securitieTQBR));
                }
                else
                {
                    Console.WriteLine($"--> Company {_repository.GetCompanyBySecId(securitieTQBR.SECID).Name} already exists");
                }
            }
        }

        private static void InitSecurities()
        {
            if (_repository.IsUpdateSecuritiesTQBR())
            {
                _repository.InitSecuritiesTQBR();
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
