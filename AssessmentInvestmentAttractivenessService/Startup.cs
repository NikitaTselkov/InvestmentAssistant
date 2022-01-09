using AssessmentInvestmentAttractivenessService.DataBase;
using AssessmentInvestmentAttractivenessService.EventProcessing;
using AssessmentInvestmentAttractivenessService.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentInvestmentAttractivenessService
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());

            builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            builder.AddJsonFile(@"Configs\DescriptionsForMultiplicators.json", optional: true, reloadOnChange: true);
            builder.AddJsonFile(@"Configs\GroupsOfMultiplicators.json", optional: true, reloadOnChange: true);
            builder.AddJsonFile(@"Configs\FieldsOfActivityOfCompany.json", optional: true, reloadOnChange: true);

            if (env.IsProduction())
            {
                builder.AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true);
            }
            else if (env.IsDevelopment())
            {
                builder.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
            }

            Configuration = builder.AddEnvironmentVariables().Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);

            Console.WriteLine("--> Using SqlServer DB");
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SQlAssessmentInvestmentAttractivenessConnection")));

            services.AddScoped<IRepository, Repository>();
            services.AddControllers();

            services.AddHostedService<MessageBusSubscriber>();

            services.AddSingleton<IEventProcessor, EventProcessor>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AssessmentInvestmentAttractivenessService", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AssessmentInvestmentAttractivenessService v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            PrebDb.InitDataBase(app);

            Console.ReadKey();
        }
    }
}
