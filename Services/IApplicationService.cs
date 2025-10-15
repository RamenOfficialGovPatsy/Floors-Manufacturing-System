using Master_Floor_Project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Master_Floor_Project.Services
{
    public interface IApplicationService
    {
        Task<IEnumerable<Application>> GetApplicationsAsync(); // Получение всех заявок
        Task<Application?> GetApplicationByIdAsync(int id); // Получение заявки по ID

        // Создание новой заявки
        Task AddApplicationAsync(Application application, List<ApplicationItem> items);
        Task UpdateApplicationAsync(Application application); // Обновление заявки
        Task DeleteApplicationAsync(int applicationId); // Удаление заявки

        // Получение позиций заявки
        Task<List<ApplicationItem>> GetApplicationItemsAsync(int applicationId);
    }
}