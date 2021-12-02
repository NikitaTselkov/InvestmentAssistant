using Microsoft.EntityFrameworkCore;
using DataParserService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataParserService.IssMoexApi.Models;

namespace DataParserService.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
        
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Multiplicator> Multiplicators { get; set; }
        public DbSet<SecuritieTQBR> SecuritiesTQBR { get; set; }
        public DbSet<CapitalizationCompany> CapitalizationCompanies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Multiplicator>()
                .HasOne(c => c.Company)
                .WithMany(c => c.Multiplicators)          
                .HasForeignKey(c => c.CompanyId);
        }
    }
}
