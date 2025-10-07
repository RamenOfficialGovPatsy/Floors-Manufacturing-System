using Master_Floor_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Master_Floor_Project.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Application> Applications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "Host=localhost;Port=5432;Username=master_floor_user;Password=RDjd6452;Database=master_floor_db";

                optionsBuilder
                    .UseNpgsql(connectionString)
                    .UseSnakeCaseNamingConvention()
                    .LogTo(message => Debug.WriteLine(message), LogLevel.Information)
                    .EnableSensitiveDataLogging();
            }
        }
    }
}