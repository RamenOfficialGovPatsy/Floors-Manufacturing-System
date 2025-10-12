using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_Floor_Project.Models;
using Master_Floor_Project.Services;
using System;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace Master_Floor_Project.ViewModels
{
    public partial class EditApplicationViewModel : ViewModelBase
    {
        private readonly IApplicationService _applicationService;
        private readonly Application _application;
        private readonly Action? _onApplicationUpdated; // ✅ Callback для обновления списка

        [ObservableProperty]
        private ObservableCollection<ApplicationItem> _applicationItems = new();

        [ObservableProperty]
        private string? _selectedStatus;

        [ObservableProperty]
        private ObservableCollection<string> _availableStatuses = new()
        {
            "Черновик",
            "В обработке",
            "Ожидает оплаты",
            "В производстве",
            "Выполнена",
            "Отменена"
        };

        [ObservableProperty]
        private string _applicationNumber = "Неизвестно";

        [ObservableProperty]
        private string _partnerName = "Неизвестно";

        [ObservableProperty]
        private DateTime _dateCreated = DateTime.Now;

        public IRelayCommand SaveCommand { get; }
        public IRelayCommand CancelCommand { get; }

        public Window? CurrentWindow { get; set; }

        // ✅ Добавляем callback в конструктор
        public EditApplicationViewModel(Application application, Action? onApplicationUpdated = null)
        {
            _application = application;
            _applicationService = new ApplicationService();
            _onApplicationUpdated = onApplicationUpdated; // ✅ Сохраняем callback

            ApplicationNumber = application.ApplicationNumber;
            PartnerName = application.PartnerName;
            DateCreated = application.DateCreated;
            SelectedStatus = application.Status;

            SaveCommand = new RelayCommand(async () => await SaveAsync());
            CancelCommand = new RelayCommand(() => Cancel());

            LoadApplicationItems();
        }

        private async void LoadApplicationItems()
        {
            try
            {
                var items = await _applicationService.GetApplicationItemsAsync(_application.ApplicationId);
                ApplicationItems.Clear();

                foreach (var item in items)
                {
                    if (item.Product != null)
                    {
                        item.ProductName = item.Product.Name;
                        item.Price = item.Product.MinPricePartner ?? 0;
                    }
                    else
                    {
                        item.ProductName = "Неизвестный продукт";
                        item.Price = 0;
                    }

                    ApplicationItems.Add(item);
                }

                Console.WriteLine($"✅ Загружено {ApplicationItems.Count} позиций заявки");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка загрузки позиций заявки: {ex.Message}");
            }
        }

        private async Task SaveAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(SelectedStatus))
                {
                    _application.Status = SelectedStatus;
                    await _applicationService.UpdateApplicationAsync(_application);
                    Console.WriteLine($"✅ Заявка {ApplicationNumber} обновлена. Новый статус: {SelectedStatus}");

                    // ✅ Вызываем callback для обновления списка заявок
                    _onApplicationUpdated?.Invoke();

                    CloseWindow();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка сохранения заявки: {ex.Message}");
            }
        }

        private void Cancel()
        {
            Console.WriteLine("❌ Редактирование отменено");
            CloseWindow();
        }

        private void CloseWindow()
        {
            CurrentWindow?.Close();
        }
    }
}