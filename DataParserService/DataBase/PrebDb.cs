using AutoMapper;
using DataParserService.Codes;
using DataParserService.DataParser;
using DataParserService.Models;
using DataParserService.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DataParserService.DataBase
{
    public static class PrebDb
    {
        private static IRepository _repository;

        public static void InitDataBase(IApplicationBuilder app)
        {
            AppDbContext appDbContext;
            IMessageBusClient messageBusClient;
            IMapper mapper;

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                messageBusClient = serviceScope.ServiceProvider.GetService<IMessageBusClient>();
                mapper = serviceScope.ServiceProvider.GetService<IMapper>();
                appDbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();

                _repository = new Repository(appDbContext, messageBusClient, mapper);

                ApplyMigrate(appDbContext);
                InitSecurities();
                InitCompanies();
            }
        }
        private static void InitCompanies()
        {
            Company company = null;

            foreach (var securitieTQBR in _repository.GetSecuritiesTQBR().Where(w => w.SECTYPE == SecuritiesTQBRCodes.Stock))
            {
                if (!_repository.IsCompanyExists(securitieTQBR))
                {
                    company = new Company(securitieTQBR);
                    _repository.AddCompany(company);
                    _repository.UpdateMultiplicatorsForCompany(company.Id);
                }
                else
                {
                    company = _repository.GetCompanyBySecId(securitieTQBR.SECID);
                    Console.WriteLine($"--> Company {company.Name} already exists");
                }

                if (company != null && _repository.IsUpdateMultiplicatorsForCompany(company.Id))
                {
                    _repository.UpdateMultiplicatorsForCompany(company.Id);
                    Console.WriteLine($"--> Multiplicators were updated for company {company.Name}");
                    company = null;
                }
            }

            Console.WriteLine("--> Initialized Companies");
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
