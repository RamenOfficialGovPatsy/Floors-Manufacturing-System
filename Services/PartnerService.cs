using Master_Floor_Project.Data;
using Master_Floor_Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System;
using System.Linq;

namespace Master_Floor_Project.Services
{
    public class PartnerService : IPartnerService
    {
        public async Task<IEnumerable<Partner>> GetPartnersAsync()
        {
            using var context = new AppDbContext();
            return await context.Partners.ToListAsync();
        }

        public async Task AddPartnerAsync(Partner partner)
        {
            try
            {
                using var context = new AppDbContext();
                context.Partners.Add(partner);
                await context.SaveChangesAsync();
                Debug.WriteLine($"🟢 PartnerService: Партнер {partner.Name} добавлен");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 PartnerService: Ошибка добавления: {ex.Message}");
                throw;
            }
        }

        public async Task UpdatePartnerAsync(Partner partner)
        {
            try
            {
                using var context = new AppDbContext();
                context.Partners.Update(partner);
                await context.SaveChangesAsync();
                Debug.WriteLine($"🟢 PartnerService: Партнер {partner.Name} обновлен");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 PartnerService: Ошибка обновления: {ex.Message}");
                throw;
            }
        }

        public async Task DeletePartnerAsync(int partnerId)
        {
            try
            {
                using var context = new AppDbContext();

                // ✅ Находим партнера со ВСЕМИ связанными данными
                var partner = await context.Partners
                    .Include(p => p.Applications)
                        .ThenInclude(a => a.ApplicationItems)
                    .FirstOrDefaultAsync(p => p.PartnerId == partnerId);

                if (partner != null)
                {
                    Console.WriteLine($"🔍 Найден партнер: {partner.Name}, заявок: {partner.Applications.Count}");

                    // 1. ✅ Удаляем историю продаж
                    var salesHistory = await context.SalesHistory
                        .Where(sh => sh.PartnerId == partnerId)
                        .ToListAsync();

                    if (salesHistory.Any())
                    {
                        context.SalesHistory.RemoveRange(salesHistory);
                        Console.WriteLine($"🗑️ Удалено {salesHistory.Count} записей истории продаж");
                    }

                    // 2. ✅ Удаляем заявки и их позиции
                    foreach (var application in partner.Applications)
                    {
                        Console.WriteLine($"🔍 Обрабатываем заявку {application.ApplicationId}, позиций: {application.ApplicationItems.Count}");

                        if (application.ApplicationItems.Any())
                        {
                            context.ApplicationItems.RemoveRange(application.ApplicationItems);
                            Console.WriteLine($"🗑️ Удалено {application.ApplicationItems.Count} позиций заявки");
                        }

                        context.Applications.Remove(application);
                        Console.WriteLine($"🗑️ Удалена заявка {application.ApplicationId}");
                    }

                    // 3. ✅ Удаляем партнера
                    context.Partners.Remove(partner);
                    await context.SaveChangesAsync();

                    Console.WriteLine($"🟢 PartnerService: Партнер {partner.Name} (ID: {partnerId}) успешно удален");
                }
                else
                {
                    Console.WriteLine($"🟡 PartnerService: Партнер {partnerId} не найден");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔴 PartnerService: Ошибка удаления партнера {partnerId}: {ex.Message}");
                Console.WriteLine($"🔴 PartnerService: Inner: {ex.InnerException?.Message}");
                throw;
            }
        }
    }
}