using Master_Floor_Project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Master_Floor_Project.Services
{
    public interface IWarehouseService
    {
        // Получение всех складских остатков
        Task<List<WarehouseItem>> GetWarehouseItemsAsync();
    }
}