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
        private readonly IWarehouseService _warehouseService;

        [ObservableProperty]
        private ObservableCollection<WarehouseItem> _warehouseItems = new();

        [ObservableProperty]
        private bool _isLoading;

        public WarehouseViewModel()
        {
            try
            {
                Debug.WriteLine("🟡 WarehouseViewModel: Инициализация...");
                _warehouseService = new WarehouseService(); // УБРАЛ параметр
                Debug.WriteLine("🟢 WarehouseViewModel: Сервис инициализирован");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 WarehouseViewModel: Ошибка инициализации: {ex.Message}");
                throw;
            }
        }

        [RelayCommand]
        public async Task LoadWarehouseDataAsync()
        {
            try
            {
                Debug.WriteLine("🟡 WarehouseViewModel: Начало загрузки данных");
                IsLoading = true;
                WarehouseItems.Clear();

                var items = await _warehouseService.GetWarehouseItemsAsync();

                if (items != null && items.Count > 0)
                {
                    foreach (var item in items)
                    {
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
                IsLoading = false;
                Debug.WriteLine("🟡 WarehouseViewModel: Загрузка завершена");
            }
        }
    }
}