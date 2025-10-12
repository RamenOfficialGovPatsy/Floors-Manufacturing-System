// File: Services/ApplicationService.cs
using Master_Floor_Project.Data;
using Master_Floor_Project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Master_Floor_Project.Services
{
    public class ApplicationService : IApplicationService
    {
        public async Task<IEnumerable<Application>> GetApplicationsAsync()
        {
            try
            {
                using var context = new AppDbContext();
                return await context.Applications.Include(a => a.Partner).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 GetApplicationsAsync Error: {ex.Message}");
                throw;
            }
        }

        public async Task<Application?> GetApplicationByIdAsync(int id)
        {
            try
            {
                using var context = new AppDbContext();
                return await context.Applications
                    .Include(a => a.Partner)
                    .Include(a => a.ApplicationItems)
                        .ThenInclude(ai => ai.Product)
                    .FirstOrDefaultAsync(a => a.ApplicationId == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 GetApplicationByIdAsync Error: {ex.Message}");
                throw;
            }
        }

        public async Task AddApplicationAsync(Application application, List<ApplicationItem> items)
        {
            try
            {
                using var context = new AppDbContext();
                using var transaction = await context.Database.BeginTransactionAsync();
                try
                {
                    context.Applications.Add(application);
                    await context.SaveChangesAsync();

                    foreach (var item in items)
                    {
                        item.ApplicationId = application.ApplicationId;
                        context.ApplicationItems.Add(item);
                    }
                    await context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 AddApplicationAsync Error: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateApplicationAsync(Application application)
        {
            try
            {
                using var context = new AppDbContext();

                // ✅ Находим существующую заявку
                var existingApplication = await context.Applications
                    .FirstOrDefaultAsync(a => a.ApplicationId == application.ApplicationId);

                if (existingApplication != null)
                {
                    // ✅ Обновляем только статус, не трогая другие поля
                    existingApplication.Status = application.Status;
                    await context.SaveChangesAsync();
                    Console.WriteLine($"🟢 Заявка ID: {application.ApplicationId} успешно обновлена. Новый статус: {application.Status}");
                }
                else
                {
                    Console.WriteLine($"🔴 Заявка ID: {application.ApplicationId} не найдена");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔴 UpdateApplicationAsync Error: {ex.Message}");
                Console.WriteLine($"🔴 Inner Exception: {ex.InnerException?.Message}");
                throw;
            }
        }

        public async Task DeleteApplicationAsync(int applicationId)
        {
            try
            {
                using var context = new AppDbContext();

                // ✅ Находим заявку вместе с позициями
                var applicationToDelete = await context.Applications
                    .Include(a => a.ApplicationItems) // Загружаем связанные позиции
                    .FirstOrDefaultAsync(a => a.ApplicationId == applicationId);

                if (applicationToDelete != null)
                {
                    Console.WriteLine($"🔍 Найдена заявка: {applicationToDelete.ApplicationNumber}, позиций: {applicationToDelete.ApplicationItems.Count}");

                    // ✅ EF автоматически удалит позиции благодаря каскадному удалению
                    context.Applications.Remove(applicationToDelete);
                    await context.SaveChangesAsync();

                    Console.WriteLine($"🟢 ApplicationService: Заявка {applicationToDelete.ApplicationNumber} (ID: {applicationId}) успешно удалена");
                }
                else
                {
                    Console.WriteLine($"🟡 ApplicationService: Заявка {applicationId} не найдена");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔴 DeleteApplicationAsync Error: {ex.Message}");
                Console.WriteLine($"🔴 Inner Exception: {ex.InnerException?.Message}");
                Console.WriteLine($"🔴 StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<List<ApplicationItem>> GetApplicationItemsAsync(int applicationId)
        {
            try
            {
                using var context = new AppDbContext();
                Console.WriteLine($"🔍 ApplicationService: Ищем позиции для заявки ID: {applicationId}");

                var items = await context.ApplicationItems
                    .Include(ai => ai.Product)
                    .Where(ai => ai.ApplicationId == applicationId)
                    .ToListAsync();

                Console.WriteLine($"🔍 ApplicationService: Найдено {items.Count} позиций для заявки {applicationId}");

                // Отладочная информация о каждой позиции
                foreach (var item in items)
                {
                    Console.WriteLine($"🔍 Позиция: ItemId={item.ApplicationItemId}, ProductId={item.ProductId}, Quantity={item.Quantity}");
                    Console.WriteLine($"🔍 Продукт: {(item.Product != null ? item.Product.Name : "NULL")}");
                    if (item.Product != null)
                    {
                        Console.WriteLine($"🔍 Цена продукта: {item.Product.MinPricePartner}");
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔴 GetApplicationItemsAsync Error: {ex.Message}");
                Console.WriteLine($"🔴 StackTrace: {ex.StackTrace}");
                throw;
            }
        }
    }
}