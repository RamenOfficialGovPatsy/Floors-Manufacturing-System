using Master_Floor_Project.Data;
using Master_Floor_Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Master_Floor_Project.Services
{
    public class ProductService : IProductService
    {
        // Получение всего каталога продукции
        public async Task<List<Product>> GetProductsAsync()
        {
            // Создание контекста базы данных для работы с продуктами
            using var context = new AppDbContext();

            // Асинхронное получение всех продуктов из базы
            return await context.Products.ToListAsync();
        }

        // Получение конкретного продукта по ID
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            // Создание нового контекста для операции поиска
            using var context = new AppDbContext();

            // Поиск продукта по первичному ключу (ID)
            return await context.Products.FindAsync(id);
        }

        // Добавление нового продукта в каталог
        public async Task AddProductAsync(Product product)
        {
            // Создание контекста для операции добавления
            using var context = new AppDbContext();

            // Добавление нового продукта в отслеживаемые сущности
            context.Products.Add(product);

            // Сохранение изменений в базе данных
            await context.SaveChangesAsync();
        }

        // Обновление данных существующего продукта
        public async Task UpdateProductAsync(Product product)
        {
            // Создание контекста для операции обновления
            using var context = new AppDbContext();

            // Помечение продукта как измененного для обновления
            context.Products.Update(product);

            // Применение изменений к базе данных
            await context.SaveChangesAsync();
        }

        // Удаление продукта из каталога по ID
        public async Task DeleteProductAsync(int productId)
        {
            // Создание контекста для операции удаления
            using var context = new AppDbContext();

            // Поиск продукта для удаления
            var product = await context.Products.FindAsync(productId);
            if (product != null) // Проверка что продукт существует в базе
            {
                // Удаление продукта из отслеживаемых сущностей
                context.Products.Remove(product);

                // Сохранение изменений в базе данных
                await context.SaveChangesAsync();
            }
        }
    }
}