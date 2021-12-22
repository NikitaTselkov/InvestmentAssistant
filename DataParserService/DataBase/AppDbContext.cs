using Microsoft.EntityFrameworkCore;
using DataParserService.Models;
using System;

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
        public DbSet<Models.Index> Indexes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Company>()
                .HasOne(c => c.SecuritieTQBR)
                .WithOne(c => c.Company)
                .HasForeignKey<Company>(c => c.SecuritieTQBRId);

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
