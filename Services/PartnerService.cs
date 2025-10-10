using Master_Floor_Project.Data;
using Master_Floor_Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System;

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

                var partner = await context.Partners.FindAsync(partnerId);
                if (partner != null)
                {
                    context.Partners.Remove(partner);
                    await context.SaveChangesAsync();
                    Debug.WriteLine($"🟢 PartnerService: Партнер {partnerId} удален");
                }
                else
                {
                    Debug.WriteLine($"🟡 PartnerService: Партнер {partnerId} не найден");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 PartnerService: Ошибка удаления партнера {partnerId}: {ex.Message}");
                Debug.WriteLine($"🔴 PartnerService: Inner: {ex.InnerException?.Message}");
                throw;
            }
        }
    }
}