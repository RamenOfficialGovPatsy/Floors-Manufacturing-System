using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_Floor_Project.Models;
using Master_Floor_Project.Services;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Master_Floor_Project.ViewModels
{
    public partial class ProductSelectionViewModel : ViewModelBase
    {
        private readonly IProductService _productService;

        [ObservableProperty]
        private ObservableCollection<Product> _allProducts = new();

        [ObservableProperty]
        private Product? _selectedProduct;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmCommand))]
        private string _quantityInput = "1";

        [ObservableProperty]
        private string _searchText = string.Empty;

        [ObservableProperty]
        private string? _validationError;

        public ObservableCollection<Product> FilteredProducts { get; set; } = new();

        public IRelayCommand ConfirmCommand { get; }
        public IRelayCommand CancelCommand { get; }

        // События для закрытия окна
        public event Action<Product?, int>? ProductSelected;
        public event Action? SelectionCancelled;

        public ProductSelectionViewModel()
        {
            _productService = new ProductService();
            ConfirmCommand = new RelayCommand(() => ConfirmSelection(), () => CanConfirmSelection());
            CancelCommand = new RelayCommand(() => CancelSelection());

            LoadProducts();
        }

        private async void LoadProducts()
        {
            try
            {
                var products = await _productService.GetProductsAsync();
                AllProducts.Clear();
                FilteredProducts.Clear();

                foreach (var product in products)
                {
                    AllProducts.Add(product);
                    FilteredProducts.Add(product);
                }

                Console.WriteLine($"✅ Загружено {AllProducts.Count} продуктов для выбора");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка загрузки продуктов: {ex.Message}");
            }
        }

        partial void OnQuantityInputChanged(string value)
        {
            // Валидируем ввод
            ValidateQuantity();
            ConfirmCommand.NotifyCanExecuteChanged();
        }

        private void ValidateQuantity()
        {
            ValidationError = null;

            if (string.IsNullOrWhiteSpace(QuantityInput))
            {
                ValidationError = "Количество обязательно для ввода";
                return;
            }

            if (!int.TryParse(QuantityInput, out int quantity))
            {
                ValidationError = "Введите корректное число";
                return;
            }

            if (quantity <= 0)
            {
                ValidationError = "Количество должно быть положительным числом";
                return;
            }
        }

        private bool CanConfirmSelection()
        {
            return SelectedProduct != null &&
                   ValidationError == null &&
                   !string.IsNullOrWhiteSpace(QuantityInput) &&
                   int.TryParse(QuantityInput, out int quantity) &&
                   quantity > 0;
        }

        partial void OnSearchTextChanged(string value)
        {
            FilterProducts();
        }

        private void FilterProducts()
        {
            FilteredProducts.Clear();

            var filtered = string.IsNullOrEmpty(SearchText)
                ? AllProducts
                : AllProducts.Where(p =>
                    (p.Name?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (p.Article?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false));

            foreach (var product in filtered)
            {
                FilteredProducts.Add(product);
            }
        }

        private void ConfirmSelection()
        {
            if (CanConfirmSelection() && int.TryParse(QuantityInput, out int quantity) && SelectedProduct != null)
            {
                Console.WriteLine($"✅ Выбран продукт: {SelectedProduct.Name}, количество: {quantity}");
                ProductSelected?.Invoke(SelectedProduct, quantity);
            }
        }

        private void CancelSelection()
        {
            Console.WriteLine("❌ Отмена выбора продукта");
            SelectionCancelled?.Invoke();
        }

        // ViewModels/ProductSelectionViewModel.cs
        partial void OnSelectedProductChanged(Product? value)
        {
            // ✅ Автоматически активируем кнопку при выборе продукта
            ConfirmCommand.NotifyCanExecuteChanged();
            Console.WriteLine($"🔄 Выбран продукт: {value?.Name}, кнопка активирована: {CanConfirmSelection()}");
        }
    }
}