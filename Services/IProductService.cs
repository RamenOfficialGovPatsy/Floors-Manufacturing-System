using Master_Floor_Project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Master_Floor_Project.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync(); // Получение всего каталога продукции
        Task<Product?> GetProductByIdAsync(int id); // Получение продукта по ID
        Task AddProductAsync(Product product); // Добавление нового продукта
        Task UpdateProductAsync(Product product); // Обновление данных продукта
        Task DeleteProductAsync(int productId);  // Удаление продукта по ID
    }
}