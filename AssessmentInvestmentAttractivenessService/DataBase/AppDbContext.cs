using AssessmentInvestmentAttractivenessService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentInvestmentAttractivenessService.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

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
        }
    }
}
