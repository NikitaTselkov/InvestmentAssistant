using AssessmentInvestmentAttractivenessService.Dtos;
using AssessmentInvestmentAttractivenessService.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentInvestmentAttractivenessService.DataBase
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _settings;
        private readonly IMapper _mapper;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration, IMapper mapper) : base(options)
        {
            _settings = configuration;
            _mapper = mapper;
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Multiplicator> Multiplicators { get; set; }
        public DbSet<Models.Index> Indexes { get; set; }
        public DbSet<FieldOfActivityOfCompany> FieldsOfActivityOfCompanies { get; set; }
        public DbSet<GroupOfMultiplicators> GroupsOfMultiplicators { get; set; }
        public DbSet<DescriptionForMultiplicators> DescriptionsForMultiplicators { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
               .Entity<Multiplicator>()
               .HasOne(c => c.Company)
               .WithMany(c => c.Multiplicators)
               .HasForeignKey(c => c.CompanyId);

            modelBuilder
              .Entity<Models.Index>()
              .HasOne(c => c.Multiplicator)
              .WithMany(c => c.Indexes)
              .HasForeignKey(c => c.MultiplicatorId);

            var descriptionsForMultiplicators = _mapper.Map<List<DescriptionForMultiplicators>>(GetListFromConfig("DescriptionsForMultiplicators"));
            modelBuilder.Entity<DescriptionForMultiplicators>().HasData(descriptionsForMultiplicators);

            var groupsOfMultiplicators = _mapper.Map<List<GroupOfMultiplicators>>(GetListFromConfig("GroupsOfMultiplicators"));
            modelBuilder.Entity<GroupOfMultiplicators>().HasData(groupsOfMultiplicators);
            
            var fieldsOfActivityOfCompany = _mapper.Map<List<FieldOfActivityOfCompany>>(GetListFromConfig("FieldsOfActivityOfCompany"));
            modelBuilder.Entity<FieldOfActivityOfCompany>().HasData(fieldsOfActivityOfCompany);
        }

        private List<DbListNodeDto> GetListFromConfig(string sectionKey)
        {
            var result = new List<DbListNodeDto>();

            try
            {
                var valuesSection = _settings.GetSection(sectionKey);
                var i = 0;
                foreach (IConfigurationSection section in valuesSection.GetChildren())
                {
                    result.Add(new DbListNodeDto
                    {
                        Id = ++i,
                        Key = section.GetValue<string>("Key"),
                        Value = section.GetValue<string>("Value")
                    });;
                }

                if (result.Count == 0) throw new ArgumentNullException();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldn't get data from config by key {sectionKey}: {ex.Message}");
            }

            return result;
        }
    }
}
