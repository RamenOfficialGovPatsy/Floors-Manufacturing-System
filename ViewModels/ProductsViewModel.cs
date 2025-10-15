using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_Floor_Project.Data;
using Master_Floor_Project.Models;
using Master_Floor_Project.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Diagnostics;
using System;

namespace Master_Floor_Project.ViewModels
{
    public partial class ProductsViewModel : ViewModelBase
    {
        // Сервис для работы с продуктами
        private readonly IProductService _productService;

        [ObservableProperty]
        private ObservableCollection<Product> _products = new(); // Коллекция продуктов для отображения

        [ObservableProperty]
        private bool _isLoading = true; // Флаг загрузки данных

        public ProductsViewModel()
        {
            Debug.WriteLine("🟡 ProductsViewModel: Конструктор вызван");

            _productService = new ProductService();
            _ = TestDatabaseConnection(); // Тестирование подключения к БД при создании
        }

        // Тестирование подключения к базе данных
        private async Task TestDatabaseConnection()
        {
            try
            {
                Debug.WriteLine("🟡 ProductsViewModel: Тестирование подключения к БД...");
                using var context = new AppDbContext(); // Создание контекста БД
                await context.TestConnectionAsync(); // Вызов метода тестирования подключения
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 ProductsViewModel: Ошибка при тестировании БД: {ex.Message}");
            }
        }

        // Команда загрузки списка продуктов из базы данных
        [RelayCommand]
        public async Task LoadProductsAsync()
        {
            Debug.WriteLine("🟡 ProductsViewModel: LoadProductsAsync начал выполнение");
            IsLoading = true; // Включение индикатора загрузки
            try
            {
                Products.Clear(); // Очистка текущего списка продуктов

                // Получение продуктов из БД
                var productsList = await _productService.GetProductsAsync();
                Debug.WriteLine($"🟡 ProductsViewModel: Получено {productsList.Count} продуктов из сервиса");

                foreach (var product in productsList)
                {
                    // Добавление продуктов в наблюдаемую коллекцию
                    Products.Add(product);
                }
                Debug.WriteLine($"🟢 ProductsViewModel: В ObservableCollection добавлено {Products.Count} продуктов");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 ProductsViewModel: Ошибка: {ex.Message}");
            }
            finally
            {
                IsLoading = false; // Выключение индикатора загрузки
                Debug.WriteLine("🟡 ProductsViewModel: LoadProductsAsync завершен");
            }
        }
    }
}