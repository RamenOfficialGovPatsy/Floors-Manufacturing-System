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
        // DbSet для работы с таблицами базы данных
        public DbSet<Partner> Partners { get; set; } // Таблица партнеров компании
        public DbSet<Product> Products { get; set; } // Таблица продукции
        public DbSet<Manager> Managers { get; set; } // Таблица менеджеров
        public DbSet<Application> Applications { get; set; } // Таблица заявок от партнеров
        public DbSet<ApplicationItem> ApplicationItems { get; set; } // Таблица позиций в заявках
        public DbSet<WarehouseItem> Warehouse { get; set; } // Таблица складских остатков
        public DbSet<SalesHistory> SalesHistory { get; set; } // Таблица истории продаж для расчета скидок

        // Тестирование подключения к БД
        public async Task TestConnectionAsync()
        {
            try
            {
                Debug.WriteLine("🟡 AppDbContext: Тестирование подключения к БД...");
                var canConnect = await Database.CanConnectAsync(); // Проверка возможности подключения
                Debug.WriteLine(canConnect
                    ? "🟢 AppDbContext: Подключение к БД успешно"
                    : "🔴 AppDbContext: Не удалось подключиться к БД");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 AppDbContext: Ошибка подключения: {ex.Message}");
            }
        }

        // Настройка подключения к БД PostgreSQL
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Проверка что конфигурация еще не выполнена
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "Host=localhost;Port=5432;Username=master_floor_user;Password=RDjd6452;Database=master_floor_db";

                optionsBuilder
                    .UseNpgsql(connectionString) // Использование PostgreSQL
                    .UseSnakeCaseNamingConvention() // Стиль именования таблиц и колонок в snake_case
                    .LogTo(message => Debug.WriteLine(message), LogLevel.Information) // Логирование SQL запросов
                    .EnableSensitiveDataLogging(); // Логирование чувствительных данных
            }
        }

        // Настройка моделей БД и связей между таблицами
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфигурация для ApplicationItem
            modelBuilder.Entity<ApplicationItem>(entity =>
            {
                entity.HasKey(ai => ai.ApplicationItemId); // Первичный ключ
                entity.HasOne(ai => ai.Application) // Связь с заявкой
                      .WithMany(a => a.ApplicationItems) // Одна заявка - много позиций
                      .HasForeignKey(ai => ai.ApplicationId) // Внешний ключ на заявку
                      .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление при удалении заявки
                entity.HasOne(ai => ai.Product) // Связь с продуктом
                      .WithMany()
                      .HasForeignKey(ai => ai.ProductId) // Внешний ключ на продукт
                      .OnDelete(DeleteBehavior.Restrict); // Запрет удаления используемого продукта
            });

            // Конфигурация для WarehouseItem
            modelBuilder.Entity<WarehouseItem>(entity =>
            {
                entity.HasKey(w => w.WarehouseId); // Первичный ключ
                entity.HasOne(w => w.Product) // Связь с продуктом
                      .WithMany()
                      .HasForeignKey(w => w.ProductId) // Внешний ключ на продукт
                      .OnDelete(DeleteBehavior.Restrict); // Запрет удаления используемого продукта
            });
        }
    }
}