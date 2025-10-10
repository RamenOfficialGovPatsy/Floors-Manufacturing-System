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
        // УБРАЛ конструктор - теперь контекст создается внутри методов

        public async Task<List<WarehouseItem>> GetWarehouseItemsAsync()
        {
            try
            {
                using var context = new AppDbContext();
                Debug.WriteLine("🟡 WarehouseService: Загрузка данных склада...");

                var items = await context.Warehouse
                    .Include(w => w.Product)
                    .ToListAsync();

                Debug.WriteLine($"🟢 WarehouseService: Загружено {items.Count} записей склада");

                for (int i = 0; i < Math.Min(3, items.Count); i++)
                {
                    var item = items[i];
                    Debug.WriteLine($"   - {item.Product?.Name ?? "No Name"}: {item.QuantityOnHand} шт.");
                }

                return items;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 WarehouseService: Ошибка загрузки: {ex.Message}");
                Debug.WriteLine($"🔴 WarehouseService: Inner: {ex.InnerException?.Message}");
                return new List<WarehouseItem>();
            }
        }
    }
}