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
              .Entity<Multiplicator>()
              .HasOne(c => c.Description)
              .WithMany(c => c.Multiplicators)
              .HasForeignKey(c => c.DescriptionId);

            modelBuilder
              .Entity<Multiplicator>()
              .HasMany(c => c.DoesNotWorkWithCompanies)
              .WithMany(c => c.Multiplicators);

            modelBuilder
              .Entity<Models.Index>()
              .HasOne(c => c.Multiplicator)
              .WithMany(c => c.Indexes)
              .HasForeignKey(c => c.MultiplicatorId);

            modelBuilder
              .Entity<DescriptionForMultiplicators>()
              .HasOne(c => c.GroupOfMultiplicators)
              .WithMany(c => c.DescriptionsForMultiplicators)
              .HasForeignKey(c => c.CodeOfGroupOfMultiplicator);

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
                var metadata = _settings.GetSection($"{sectionKey}:metadata").Get<string[]>();
                var valuesSection = _settings.GetSection($"{sectionKey}:data");
                var i = 0;

                var length = metadata.Length;            

                foreach (IConfigurationSection section in valuesSection.GetChildren())
                {
                    var iterator = 0;
                    var listNode = new DbListNodeDto() { Id = ++i };

                    listNode.Keys = new string[length];
                    listNode.Values = new string[length];

                    foreach (var key in metadata)
                    {
                        listNode.Keys[iterator] = key;
                        listNode.Values[iterator] = section.GetValue<string>(key);
                        iterator++;
                    }

                    result.Add(listNode);
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
