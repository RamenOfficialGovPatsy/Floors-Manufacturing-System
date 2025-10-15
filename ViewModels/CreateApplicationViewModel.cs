using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_Floor_Project.Models;
using Master_Floor_Project.Services;
using Master_Floor_Project.Windows;
using System;
using Avalonia.Controls;

namespace Master_Floor_Project.ViewModels
{
    public partial class CreateApplicationViewModel : ViewModelBase
    {
        private readonly IDiscountService _discountService; // Сервис расчета скидок
        private readonly IPartnerService _partnerService; // Сервис работы с партнерами
        private readonly IProductService _productService; // Сервис работы с продуктами
        private readonly IApplicationService _applicationService; // Сервис работы с заявками

        [ObservableProperty]
        private decimal _subTotal; // Общая сумма заявки без скидки

        [ObservableProperty]
        private decimal _discountPercentage; // Процент скидки

        [ObservableProperty]
        private decimal _finalTotal; // Итоговая сумма со скидкой

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PartnerValidationError))]
        [NotifyCanExecuteChangedFor(nameof(SaveApplicationCommand))]
        private Partner? _selectedPartner; // Выбранный партнер для заявки

        [ObservableProperty]
        private DateTime _dateCreated = DateTime.Now; // Дата создания заявки

        [ObservableProperty]
        private bool _isLoading = true; // Флаг загрузки данных

        [ObservableProperty]
        private string? _partnerValidationError; // Сообщение об ошибке валидации партнера

        public Window? CurrentWindow { get; set; } // Ссылка на текущее окно

        // Список всех партнеров
        public ObservableCollection<Partner> AllPartners { get; set; } = new();

        // Список всех продуктов
        public ObservableCollection<Product> AllProducts { get; set; } = new();

        // Позиции текущей заявки
        public ObservableCollection<ApplicationItem> Items { get; set; } = new();

        // Событие успешного создания заявки
        public event Action? OnApplicationCreated;

        public IRelayCommand SaveApplicationCommand { get; } // Команда сохранения заявки
        public IRelayCommand AddProductCommand { get; } // Команда добавления продукта
        public IRelayCommand CancelCommand { get; } // Команда отмены создания

        public CreateApplicationViewModel()
        {
            _discountService = new DiscountService();
            _partnerService = new PartnerService();
            _productService = new ProductService();
            _applicationService = new ApplicationService();

            // Инициализация команд с проверкой возможности выполнения
            SaveApplicationCommand = new RelayCommand(() => SaveApplicationToDatabase(), () => CanSaveApplication());
            AddProductCommand = new RelayCommand(() => AddProduct());
            CancelCommand = new RelayCommand(() => Cancel());

            LoadInitialData(); // Загрузка начальных данных
        }

        // Проверка возможности сохранения заявки
        private bool CanSaveApplication()
        {
            ValidatePartner();
            return SelectedPartner != null && Items.Any(); // Партнер выбран и есть позиции
        }

        // Валидация выбора партнера
        private void ValidatePartner()
        {
            PartnerValidationError = SelectedPartner == null ? "Необходимо выбрать партнера" : null;
        }

        // Обработчик изменения выбранного партнера
        partial void OnSelectedPartnerChanged(Partner? value)
        {
            // Автоматически валидируем при изменении выбора партнера
            ValidatePartner();
            SaveApplicationCommand.NotifyCanExecuteChanged(); // Обновление состояния команды
        }

        // Загрузка начальных данных (партнеры и продукты)
        private async void LoadInitialData()
        {
            try
            {
                IsLoading = true;
                Console.WriteLine("🟡 Начинаем загрузку данных для создания заявки...");

                // Загрузка списка партнеров из базы данных
                var partners = await _partnerService.GetPartnersAsync();
                AllPartners.Clear();
                foreach (var partner in partners)
                {
                    AllPartners.Add(partner);
                }
                Console.WriteLine($"✅ Загружено {AllPartners.Count} партнеров");

                // Загрузка списка продуктов из базы данных
                var products = await _productService.GetProductsAsync();
                AllProducts.Clear();
                foreach (var product in products)
                {
                    AllProducts.Add(product);
                }
                Console.WriteLine($"✅ Загружено {AllProducts.Count} продуктов");

                Items.Clear(); // Очистка позиций заявки
                Console.WriteLine("✅ Таблица продуктов заявки очищена");

                CalculateTotals(); // Расчет начальных сумм
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка загрузки данных: {ex.Message}");
                Console.WriteLine($"❌ StackTrace: {ex.StackTrace}");
            }
            finally
            {
                IsLoading = false;
                Console.WriteLine("🟢 Загрузка данных завершена");
            }
        }

        // Расчет итоговых сумм заявки
        private void CalculateTotals()
        {
            SubTotal = Items.Sum(item => item.Sum); // Сумма без скидки
            DiscountPercentage = _discountService.CalculateDiscount(SubTotal); // Расчет скидки
            FinalTotal = SubTotal * (1 - DiscountPercentage); // Итоговая сумма со скидкой

            // Обновление состояния кнопки сохранения
            SaveApplicationCommand.NotifyCanExecuteChanged();

            Console.WriteLine($"💰 Расчет сумм: SubTotal={SubTotal}, Discount={DiscountPercentage:P0}, Final={FinalTotal}");
        }

        // Добавление продукта в заявку
        private void AddProduct()
        {
            Console.WriteLine("➕ Открываем окно выбора продукта...");
            ShowProductSelectionWindow();
        }

        // Отмена создания заявки
        private void Cancel()
        {
            Console.WriteLine("❌ Отмена создания заявки...");
            CloseWindow();
        }

        // Открытие окна выбора продукции
        private void ShowProductSelectionWindow()
        {
            var selectionViewModel = new ProductSelectionViewModel();
            var window = new ProductSelectionWindow
            {
                DataContext = selectionViewModel
            };

            // Подписка на событие выбора продукта
            selectionViewModel.ProductSelected += (product, quantity) =>
            {
                if (product != null)
                {
                    // Добавление выбранного продукта
                    AddProductToApplication(product, quantity);
                }
                window.Close();
            };

            // Подписка на событие отмены выбора
            selectionViewModel.SelectionCancelled += () =>
            {
                window.Close();
            };

            window.Show();
        }

        // Добавление продукта в коллекцию позиций заявки
        private void AddProductToApplication(Product product, int quantity)
        {
            // Проверяем, не добавлен ли уже этот продукт
            var existingItem = Items.FirstOrDefault(item => item.ProductId == product.ProductId);

            if (existingItem != null)
            {
                // Если продукт уже есть - увеличиваем количество
                existingItem.Quantity += quantity;
                Console.WriteLine($"📦 Увеличено количество продукта {product.Name} на {quantity}");
            }
            else
            {
                // Создание новой позиции заявки
                var newItem = new ApplicationItem
                {
                    ProductId = product.ProductId,
                    Product = product,
                    ProductName = product.Name,
                    Price = product.MinPricePartner ?? 0,
                    Quantity = quantity
                };

                Items.Add(newItem); // Добавление новой позиции
                Console.WriteLine($"🆕 Добавлен продукт: {product.Name}, количество: {quantity}, цена: {newItem.Price}");
            }

            // Пересчет сумм после добавления продукта
            CalculateTotals();
        }

        // Сохранение заявки в базу данных
        private async void SaveApplicationToDatabase()
        {
            if (!CanSaveApplication())
            {
                Console.WriteLine("❌ Невозможно сохранить заявку: не выбран партнер или нет продуктов");
                return;
            }

            try
            {
                Console.WriteLine($"🔍 Начинаем сохранение заявки...");
                Console.WriteLine($"🔍 Партнер: {SelectedPartner!.Name} (ID: {SelectedPartner.PartnerId})");
                Console.WriteLine($"🔍 Продуктов в заявке: {Items.Count}");

                // Создание объекта заявки
                var application = new Application
                {
                    PartnerId = SelectedPartner.PartnerId,
                    ManagerId = 1, // ID менеджера по умолчанию
                    DateCreated = DateTime.UtcNow,
                    Status = "Черновик"
                };

                // Сохранение в БД
                await _applicationService.AddApplicationAsync(application, Items.ToList());
                Console.WriteLine($"✅ Заявка успешно создана: {application.ApplicationNumber}");

                OnApplicationCreated?.Invoke(); // Вызов события успешного создания
                CloseWindow(); // Закрытие окна
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка сохранения заявки: {ex.Message}");
            }
        }

        // Закрытие текущего окна
        private void CloseWindow()
        {
            CurrentWindow?.Close();
        }
    }
}