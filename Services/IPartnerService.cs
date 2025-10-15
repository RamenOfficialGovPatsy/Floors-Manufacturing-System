using Master_Floor_Project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Master_Floor_Project.Services
{
    public interface IPartnerService
    {
        Task<IEnumerable<Partner>> GetPartnersAsync(); // Получение всех партнеров
        Task AddPartnerAsync(Partner partner); // Добавление нового партнера
        Task UpdatePartnerAsync(Partner partner); // Обновление данных партнера
        Task DeletePartnerAsync(int partnerId); // Удаление партнера по ID
    }
}