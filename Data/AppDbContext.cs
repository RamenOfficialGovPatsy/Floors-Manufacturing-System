using Master_Floor_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Master_Floor_Project.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Application> Applications { get; set; }
        // УДАЛИЛ: public DbSet<ApplicationItem> ApplicationItems { get; set; }
        public DbSet<WarehouseItem> Warehouse { get; set; }

        public async Task TestConnectionAsync()
        {
            try
            {
                Debug.WriteLine("🟡 AppDbContext: Тестирование подключения к БД...");
                var canConnect = await Database.CanConnectAsync();
                Debug.WriteLine(canConnect
                    ? "🟢 AppDbContext: Подключение к БД успешно"
                    : "🔴 AppDbContext: Не удалось подключиться к БД");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 AppDbContext: Ошибка подключения: {ex.Message}");
            }
        }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ТОЛЬКО WarehouseItem конфигурация
            modelBuilder.Entity<WarehouseItem>(entity =>
            {
                entity.HasKey(w => w.WarehouseId);
                entity.HasOne(w => w.Product)
                      .WithMany()
                      .HasForeignKey(w => w.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}