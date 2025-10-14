using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_Floor_Project.Models;
using Master_Floor_Project.Services;
using Master_Floor_Project.Windows;
using System;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace Master_Floor_Project.ViewModels
{
    public partial class CreateApplicationViewModel : ViewModelBase
    {
        private readonly IDiscountService _discountService;
        private readonly IPartnerService _partnerService;
        private readonly IProductService _productService;
        private readonly IApplicationService _applicationService;

        [ObservableProperty]
        private decimal _subTotal;

        [ObservableProperty]
        private decimal _discountPercentage;

        [ObservableProperty]
        private decimal _finalTotal;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PartnerValidationError))]
        [NotifyCanExecuteChangedFor(nameof(SaveApplicationCommand))]
        private Partner? _selectedPartner;

        [ObservableProperty]
        private DateTime _dateCreated = DateTime.Now;

        [ObservableProperty]
        private bool _isLoading = true;

        // ✅ Добавляем свойство для ошибки валидации партнера
        [ObservableProperty]
        private string? _partnerValidationError;

        public Window? CurrentWindow { get; set; }

        public ObservableCollection<Partner> AllPartners { get; set; } = new();
        public ObservableCollection<Product> AllProducts { get; set; } = new();
        public ObservableCollection<ApplicationItem> Items { get; set; } = new();

        public event Action? OnApplicationCreated;

        // ✅ Добавляем команды
        public IRelayCommand SaveApplicationCommand { get; }
        public IRelayCommand AddProductCommand { get; }
        public IRelayCommand CancelCommand { get; }

        public CreateApplicationViewModel()
        {
            _discountService = new DiscountService();
            _partnerService = new PartnerService();
            _productService = new ProductService();
            _applicationService = new ApplicationService();

            // ✅ Инициализируем ВСЕ команды
            SaveApplicationCommand = new RelayCommand(() => SaveApplicationToDatabase(), () => CanSaveApplication());
            AddProductCommand = new RelayCommand(() => AddProduct());
            CancelCommand = new RelayCommand(() => Cancel());

            LoadInitialData();
        }

        // ✅ Метод проверки возможности сохранения
        private bool CanSaveApplication()
        {
            ValidatePartner();
            return SelectedPartner != null && Items.Any();
        }

        // ✅ Валидация партнера
        private void ValidatePartner()
        {
            PartnerValidationError = SelectedPartner == null ? "Необходимо выбрать партнера" : null;
        }

        partial void OnSelectedPartnerChanged(Partner? value)
        {
            // ✅ Автоматически валидируем при изменении выбора партнера
            ValidatePartner();
            SaveApplicationCommand.NotifyCanExecuteChanged();
        }

        private async void LoadInitialData()
        {
            try
            {
                IsLoading = true;
                Console.WriteLine("🟡 Начинаем загрузку данных для создания заявки...");

                // ✅ Загружаем реальных партнеров из БД
                var partners = await _partnerService.GetPartnersAsync();
                AllPartners.Clear();
                foreach (var partner in partners)
                {
                    AllPartners.Add(partner);
                }
                Console.WriteLine($"✅ Загружено {AllPartners.Count} партнеров");

                // ✅ Загружаем реальные продукты из БД
                var products = await _productService.GetProductsAsync();
                AllProducts.Clear();
                foreach (var product in products)
                {
                    AllProducts.Add(product);
                }
                Console.WriteLine($"✅ Загружено {AllProducts.Count} продуктов");

                Items.Clear();
                Console.WriteLine("✅ Таблица продуктов заявки очищена");

                CalculateTotals();
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

        private void CalculateTotals()
        {
            SubTotal = Items.Sum(item => item.Sum);
            DiscountPercentage = _discountService.CalculateDiscount(SubTotal);
            FinalTotal = SubTotal * (1 - DiscountPercentage);

            // ✅ Обновляем состояние кнопки сохранения при изменении продуктов
            SaveApplicationCommand.NotifyCanExecuteChanged();

            Console.WriteLine($"💰 Расчет сумм: SubTotal={SubTotal}, Discount={DiscountPercentage:P0}, Final={FinalTotal}");
        }

        private void AddProduct()
        {
            Console.WriteLine("➕ Открываем окно выбора продукта...");
            ShowProductSelectionWindow();
        }

        private void Cancel()
        {
            Console.WriteLine("❌ Отмена создания заявки...");
            CloseWindow();
        }

        private void ShowProductSelectionWindow()
        {
            var selectionViewModel = new ProductSelectionViewModel();
            var window = new ProductSelectionWindow
            {
                DataContext = selectionViewModel
            };

            // Подписываемся на события выбора
            selectionViewModel.ProductSelected += (product, quantity) =>
            {
                if (product != null)
                {
                    AddProductToApplication(product, quantity);
                }
                window.Close();
            };

            selectionViewModel.SelectionCancelled += () =>
            {
                window.Close();
            };

            window.Show();
        }

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
                // Добавляем новый продукт
                var newItem = new ApplicationItem
                {
                    ProductId = product.ProductId,
                    Product = product,
                    ProductName = product.Name,
                    Price = product.MinPricePartner ?? 0,
                    Quantity = quantity
                };

                Items.Add(newItem);
                Console.WriteLine($"🆕 Добавлен продукт: {product.Name}, количество: {quantity}, цена: {newItem.Price}");
            }

            // Пересчитываем суммы и обновляем состояние кнопки
            CalculateTotals();
        }

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

                var application = new Application
                {
                    PartnerId = SelectedPartner.PartnerId,
                    ManagerId = 1,
                    DateCreated = DateTime.UtcNow,
                    Status = "Черновик"
                };

                await _applicationService.AddApplicationAsync(application, Items.ToList());
                Console.WriteLine($"✅ Заявка успешно создана: {application.ApplicationNumber}");

                OnApplicationCreated?.Invoke();
                CloseWindow();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка сохранения заявки: {ex.Message}");
            }
        }

        private void CloseWindow()
        {
            CurrentWindow?.Close();
        }
    }
}