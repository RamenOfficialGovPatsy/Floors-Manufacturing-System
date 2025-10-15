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
        // Получение всех заявок с информацией о партнерах
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

        // Получение заявки по ID с полной информацией о партнере и позициях
        public async Task<Application?> GetApplicationByIdAsync(int id)
        {
            try
            {
                using var context = new AppDbContext();
                return await context.Applications
                    .Include(a => a.Partner) // Загружаем данные партнера
                    .Include(a => a.ApplicationItems) // Загружаем позиции заявки
                        .ThenInclude(ai => ai.Product) // Загружаем информацию о продуктах в позициях
                    .FirstOrDefaultAsync(a => a.ApplicationId == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 GetApplicationByIdAsync Error: {ex.Message}");
                throw;
            }
        }

        // Добавление новой заявки с позициями
        // Использует транзакцию для обеспечения целостности данных
        public async Task AddApplicationAsync(Application application, List<ApplicationItem> items)
        {
            try
            {
                using var context = new AppDbContext();

                // Начало транзакции
                using var transaction = await context.Database.BeginTransactionAsync();
                try
                {
                    // Добавляем заявку в базу данных
                    context.Applications.Add(application);
                    await context.SaveChangesAsync(); // Сохраняем чтобы получить ApplicationId

                    // Добавляем все позиции заявки
                    foreach (var item in items)
                    {
                        // Устанавливаем связь с заявкой
                        item.ApplicationId = application.ApplicationId;

                        // Привязываем существующий продукт из базы данных
                        var existingProduct = await context.Products.FindAsync(item.ProductId);
                        if (existingProduct != null)
                        {
                            item.Product = existingProduct; // Привязываем существующий продукт
                        }

                        context.ApplicationItems.Add(item); // Добавляем позицию в контекст
                    }
                    await context.SaveChangesAsync(); // Сохраняем все позиции
                    await transaction.CommitAsync(); // Подтверждаем транзакцию
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(); // Откатываем транзакцию при ошибке
                    throw;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 AddApplicationAsync Error: {ex.Message}");
                throw;
            }
        }

        // Обновление статуса заявки
        public async Task UpdateApplicationAsync(Application application)
        {
            try
            {
                using var context = new AppDbContext();

                // Находим существующую заявку
                var existingApplication = await context.Applications
                    .FirstOrDefaultAsync(a => a.ApplicationId == application.ApplicationId);

                if (existingApplication != null)
                {
                    // Обновляем только статус 
                    existingApplication.Status = application.Status;
                    await context.SaveChangesAsync();
                    Debug.WriteLine($"🟢 Заявка ID: {application.ApplicationId} успешно обновлена. Новый статус: {application.Status}");
                }
                else
                {
                    Debug.WriteLine($"🔴 Заявка ID: {application.ApplicationId} не найдена");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 UpdateApplicationAsync Error: {ex.Message}");
                Debug.WriteLine($"🔴 Inner Exception: {ex.InnerException?.Message}");
                throw;
            }
        }

        // Удаление заявки и всех связанных позиций
        // Использует каскадное удаление для позиций заявки
        public async Task DeleteApplicationAsync(int applicationId)
        {
            try
            {
                using var context = new AppDbContext();

                // Находим заявку вместе с позициями
                var applicationToDelete = await context.Applications
                    .Include(a => a.ApplicationItems) // Загружаем связанные позиции
                    .FirstOrDefaultAsync(a => a.ApplicationId == applicationId);

                if (applicationToDelete != null)
                {
                    Debug.WriteLine($"🔍 Найдена заявка: {applicationToDelete.ApplicationNumber}, позиций: {applicationToDelete.ApplicationItems.Count}");

                    // Удаляем заявку (позиции удалятся каскадно)
                    context.Applications.Remove(applicationToDelete);
                    await context.SaveChangesAsync();

                    Debug.WriteLine($"🟢 ApplicationService: Заявка {applicationToDelete.ApplicationNumber} (ID: {applicationId}) успешно удалена");
                }
                else
                {
                    Debug.WriteLine($"🟡 ApplicationService: Заявка {applicationId} не найдена");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 DeleteApplicationAsync Error: {ex.Message}");
                Debug.WriteLine($"🔴 Inner Exception: {ex.InnerException?.Message}");
                Debug.WriteLine($"🔴 StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        // Получение всех позиций заявки с информацией о продуктах
        public async Task<List<ApplicationItem>> GetApplicationItemsAsync(int applicationId)
        {
            try
            {
                using var context = new AppDbContext();
                Debug.WriteLine($"🔍 ApplicationService: Ищем позиции для заявки ID: {applicationId}");

                var items = await context.ApplicationItems
                    .Include(ai => ai.Product) // Загружаем информацию о продуктах
                    .Where(ai => ai.ApplicationId == applicationId) // Фильтруем по ID заявки
                    .ToListAsync();

                Debug.WriteLine($"🔍 ApplicationService: Найдено {items.Count} позиций для заявки {applicationId}");

                // Отладочная информация о каждой позиции
                foreach (var item in items)
                {
                    Debug.WriteLine($"🔍 Позиция: ItemId={item.ApplicationItemId}, ProductId={item.ProductId}, Quantity={item.Quantity}");
                    Debug.WriteLine($"🔍 Продукт: {(item.Product != null ? item.Product.Name : "NULL")}");
                    if (item.Product != null)
                    {
                        Debug.WriteLine($"🔍 Цена продукта: {item.Product.MinPricePartner}");
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 GetApplicationItemsAsync Error: {ex.Message}");
                Debug.WriteLine($"🔴 StackTrace: {ex.StackTrace}");
                throw;
            }
        }
    }
}