using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_Floor_Project.Models;
using Master_Floor_Project.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Diagnostics;
using System;

namespace Master_Floor_Project.ViewModels
{
    public partial class WarehouseViewModel : ViewModelBase
    {
        // Сервис для работы со складом
        private readonly IWarehouseService _warehouseService;

        [ObservableProperty]
        private ObservableCollection<WarehouseItem> _warehouseItems = new(); // Коллекция складских остатков

        [ObservableProperty]
        private bool _isLoading; // Флаг загрузки данных

        public WarehouseViewModel()
        {
            try
            {
                Debug.WriteLine("🟡 WarehouseViewModel: Инициализация...");
                _warehouseService = new WarehouseService(); // Создание сервиса для работы со складом
                Debug.WriteLine("🟢 WarehouseViewModel: Сервис инициализирован");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 WarehouseViewModel: Ошибка инициализации: {ex.Message}");
                throw;
            }
        }

        // Команда загрузки данных о складских остатках
        [RelayCommand]
        public async Task LoadWarehouseDataAsync()
        {
            try
            {
                Debug.WriteLine("🟡 WarehouseViewModel: Начало загрузки данных");
                IsLoading = true; // Включение индикатора загрузки
                WarehouseItems.Clear(); // Очистка текущего списка остатков

                // Получение данных из БД
                var items = await _warehouseService.GetWarehouseItemsAsync();

                // Проверка что данные получены
                if (items != null && items.Count > 0)
                {
                    foreach (var item in items)
                    {
                        // Добавление остатков в коллекцию
                        WarehouseItems.Add(item);
                    }
                    Debug.WriteLine($"🟢 WarehouseViewModel: Загружено {WarehouseItems.Count} записей");
                }
                else
                {
                    Debug.WriteLine("🟡 WarehouseViewModel: Нет данных для отображения");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 WarehouseViewModel: Ошибка загрузки: {ex.Message}");
            }
            finally
            {
                IsLoading = false; // Выключение индикатора загрузки
                Debug.WriteLine("🟡 WarehouseViewModel: Загрузка завершена");
            }
        }
    }
}