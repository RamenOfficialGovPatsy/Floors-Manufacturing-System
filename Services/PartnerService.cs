using System.Collections.Generic;
using System.Threading.Tasks;
using Master_Floor_Project.Data;
using Master_Floor_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Master_Floor_Project.Services
{
    public class PartnerService : IPartnerService
    {
        public async Task<List<Partner>> GetPartnersAsync()
        {
            // Создаем экземпляр  контекста
            await using var dbContext = new AppDbContext();

            // С помощью EF Core получаем всех партнеров из таблицы Partners
            // и возвращаем их в виде списка.
            return await dbContext.Partners.ToListAsync();
        }
    }
}