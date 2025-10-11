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
                // Используем Update, чтобы EF Core отследил изменения в статусе
                context.Applications.Update(application);
                await context.SaveChangesAsync();
                Debug.WriteLine($"🟢 Заявка ID: {application.ApplicationId} успешно обновлена.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 UpdateApplicationAsync Error: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteApplicationAsync(int applicationId)
        {
            try
            {
                using var context = new AppDbContext();
                // Находим заявку вместе с ее позициями
                var applicationToDelete = await context.Applications
                    .Include(a => a.ApplicationItems)
                    .FirstOrDefaultAsync(a => a.ApplicationId == applicationId);

                if (applicationToDelete != null)
                {
                    Debug.WriteLine($"Найдены {applicationToDelete.ApplicationItems.Count} позиций для удаления.");
                    // Сначала удаляем связанные позиции
                    if (applicationToDelete.ApplicationItems.Any())
                    {
                        context.ApplicationItems.RemoveRange(applicationToDelete.ApplicationItems);
                    }

                    // Затем удаляем саму заявку
                    context.Applications.Remove(applicationToDelete);

                    await context.SaveChangesAsync();
                    Debug.WriteLine($"🟢 Заявка ID: {applicationId} и ее позиции успешно удалены.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 DeleteApplicationAsync Error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ApplicationItem>> GetApplicationItemsAsync(int applicationId)
        {
            try
            {
                using var context = new AppDbContext();
                return await context.ApplicationItems
                    .Include(ai => ai.Product)
                    .Where(ai => ai.ApplicationId == applicationId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 GetApplicationItemsAsync Error: {ex.Message}");
                throw;
            }
        }
    }
}