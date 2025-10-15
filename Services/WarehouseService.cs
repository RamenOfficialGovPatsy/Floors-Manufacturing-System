using Master_Floor_Project.Data;
using Master_Floor_Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System;

namespace Master_Floor_Project.Services
{
    public class WarehouseService : IWarehouseService
    {
        // Получение всех складских остатков с информацией о продуктах
        public async Task<List<WarehouseItem>> GetWarehouseItemsAsync()
        {
            try
            {
                // Создание контекста для работы со складом
                using var context = new AppDbContext();
                Debug.WriteLine("🟡 WarehouseService: Загрузка данных склада...");

                var items = await context.Warehouse // Запрос к таблице складских остатков
                    .Include(w => w.Product) // Загружаем информацию о продуктах
                    .ToListAsync(); // Выполнение запроса и преобразование в список

                Debug.WriteLine($"🟢 WarehouseService: Загружено {items.Count} записей склада");

                // Логируем первые 3 записи для отладки
                for (int i = 0; i < Math.Min(3, items.Count); i++)
                {
                    // Получение конкретного элемента из списка складских остатков
                    var item = items[i];
                    Debug.WriteLine($"   - {item.Product?.Name ?? "No Name"}: {item.QuantityOnHand} шт.");
                }

                // Возврат списка всех складских остатков с информацией о продуктах
                return items;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 WarehouseService: Ошибка загрузки: {ex.Message}");
                Debug.WriteLine($"🔴 WarehouseService: Inner: {ex.InnerException?.Message}");

                // Возврат пустого списка при возникновении ошибки
                return new List<WarehouseItem>();
            }
        }
    }
}