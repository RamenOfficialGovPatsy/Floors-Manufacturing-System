// ViewModels/ProductsViewModel.cs
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
        private readonly IProductService _productService;

        [ObservableProperty]
        private ObservableCollection<Product> _products = new();

        [ObservableProperty]
        private bool _isLoading = true;

        public ProductsViewModel()
        {
            Debug.WriteLine("🟡 ProductsViewModel: Конструктор вызван");

            _productService = new ProductService(new AppDbContext());
            _ = TestDatabaseConnection();
        }

        private async Task TestDatabaseConnection()
        {
            try
            {
                Debug.WriteLine("🟡 ProductsViewModel: Тестирование подключения к БД...");
                using var context = new AppDbContext(); // ПРАВИЛЬНО!
                await context.TestConnectionAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 ProductsViewModel: Ошибка при тестировании БД: {ex.Message}");
            }
        }

        [RelayCommand]
        public async Task LoadProductsAsync()
        {
            Debug.WriteLine("🟡 ProductsViewModel: LoadProductsAsync начал выполнение");
            IsLoading = true;
            try
            {
                Products.Clear();
                var productsList = await _productService.GetProductsAsync();
                Debug.WriteLine($"🟡 ProductsViewModel: Получено {productsList.Count} продуктов из сервиса");

                foreach (var product in productsList)
                {
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
                IsLoading = false;
                Debug.WriteLine("🟡 ProductsViewModel: LoadProductsAsync завершен");
            }
        }
    }
}