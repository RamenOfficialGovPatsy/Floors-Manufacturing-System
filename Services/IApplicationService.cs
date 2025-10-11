// File: Services/IApplicationService.cs
using Master_Floor_Project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Master_Floor_Project.Services
{
    public interface IApplicationService
    {
        Task<IEnumerable<Application>> GetApplicationsAsync();
        Task<Application?> GetApplicationByIdAsync(int id);
        Task AddApplicationAsync(Application application, List<ApplicationItem> items);
        Task UpdateApplicationAsync(Application application);
        Task DeleteApplicationAsync(int applicationId);
        Task<List<ApplicationItem>> GetApplicationItemsAsync(int applicationId);
    }
}