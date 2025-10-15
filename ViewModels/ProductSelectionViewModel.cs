using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_Floor_Project.Models;
using Master_Floor_Project.Services;

namespace Master_Floor_Project.ViewModels
{
    // ViewModel для окна выбора продукции при создании заявки
    public partial class ProductSelectionViewModel : ViewModelBase
    {
        // Сервис для работы с продуктами
        private readonly IProductService _productService;

        [ObservableProperty]
        private ObservableCollection<Product> _allProducts = new(); // Полный список всех продуктов

        [ObservableProperty]
        private Product? _selectedProduct; // Выбранный продукт в списке

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmCommand))]
        private string _quantityInput = "1"; // Введенное количество (по умолчанию 1)

        [ObservableProperty]
        private string _searchText = string.Empty; // Текст для поиска продуктов

        [ObservableProperty]
        private string? _validationError; // Сообщение об ошибке валидации количества

        // Отфильтрованный список продуктов
        public ObservableCollection<Product> FilteredProducts { get; set; } = new();

        public IRelayCommand ConfirmCommand { get; } // Команда подтверждения выбора
        public IRelayCommand CancelCommand { get; } // Команда отмены выбора

        // События для коммуникации с родительским окном
        public event Action<Product?, int>? ProductSelected; // Событие выбора продукта
        public event Action? SelectionCancelled; // Событие отмены выбора

        public ProductSelectionViewModel()
        {
            _productService = new ProductService();

            // Инициализация команд с проверкой возможности выполнения
            ConfirmCommand = new RelayCommand(() => ConfirmSelection(), () => CanConfirmSelection());
            CancelCommand = new RelayCommand(() => CancelSelection());

            LoadProducts(); // Загрузка списка продуктов при создании
        }

        // Загрузка всех продуктов из базы данных
        private async void LoadProducts()
        {
            try
            {
                // Получение продуктов из БД
                var products = await _productService.GetProductsAsync();
                AllProducts.Clear();
                FilteredProducts.Clear();

                foreach (var product in products)
                {
                    AllProducts.Add(product); // Добавление в полный список
                    FilteredProducts.Add(product);  // Добавление в отфильтрованный список
                }

                Console.WriteLine($"✅ Загружено {AllProducts.Count} продуктов для выбора");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка загрузки продуктов: {ex.Message}");
            }
        }

        // Обработчик изменения введенного количества
        partial void OnQuantityInputChanged(string value)
        {
            // Валидируем ввод
            ValidateQuantity(); // Валидация нового значения
            ConfirmCommand.NotifyCanExecuteChanged(); // Обновление состояния кнопки подтверждения
        }

        // Валидация введенного количества
        private void ValidateQuantity()
        {
            ValidationError = null; // Сброс предыдущей ошибки

            if (string.IsNullOrWhiteSpace(QuantityInput))
            {
                ValidationError = "Количество обязательно для ввода";
                return;
            }

            // Проверка что введено число
            if (!int.TryParse(QuantityInput, out int quantity))
            {
                ValidationError = "Введите корректное число";
                return;
            }

            // Проверка что количество положительное
            if (quantity <= 0)
            {
                ValidationError = "Количество должно быть положительным числом";
                return;
            }
        }

        // Проверка возможности подтверждения выбора
        private bool CanConfirmSelection()
        {
            return SelectedProduct != null && // Продукт выбран
                   ValidationError == null && // Нет ошибок валидации
                   !string.IsNullOrWhiteSpace(QuantityInput) && // Количество не пустое
                   int.TryParse(QuantityInput, out int quantity) && // Количество - число
                   quantity > 0; // Количество положительное
        }

        // Обработчик изменения текста поиска
        partial void OnSearchTextChanged(string value)
        {
            FilterProducts(); // Фильтрация списка при изменении поискового запроса
        }

        // Фильтрация списка продуктов по названию или артикулу
        private void FilterProducts()
        {
            FilteredProducts.Clear(); // Очистка текущего отфильтрованного списка

            var filtered = string.IsNullOrEmpty(SearchText)
                ? AllProducts // Если поиск пустой - показываем все продукты
                : AllProducts.Where(p =>
                    (p.Name?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) || // Поиск по названию
                    (p.Article?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false)); // Поиск по артикулу

            foreach (var product in filtered)
            {
                FilteredProducts.Add(product); // Добавление отфильтрованных продуктов
            }
        }

        // Подтверждение выбора продукта
        private void ConfirmSelection()
        {
            if (CanConfirmSelection() && int.TryParse(QuantityInput, out int quantity) && SelectedProduct != null)
            {
                Console.WriteLine($"✅ Выбран продукт: {SelectedProduct.Name}, количество: {quantity}");
                ProductSelected?.Invoke(SelectedProduct, quantity);  // Вызов события с выбранным продуктом и количеством
            }
        }

        // Отмена выбора продукта
        private void CancelSelection()
        {
            Console.WriteLine("❌ Отмена выбора продукта");
            SelectionCancelled?.Invoke(); // Вызов события отмены
        }

        // Обработчик изменения выбранного продукта
        partial void OnSelectedProductChanged(Product? value)
        {
            // Обновление состояния кнопки при выборе продукта
            ConfirmCommand.NotifyCanExecuteChanged();
            Console.WriteLine($"🔄 Выбран продукт: {value?.Name}, кнопка активирована: {CanConfirmSelection()}");
        }
    }
}