using Microsoft.EntityFrameworkCore;
using DataParserService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataParserService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
        
        }

        public DbSet<PriceIndex> PriceIndices { get; set; }
    }
}
