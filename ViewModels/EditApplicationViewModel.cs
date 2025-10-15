using System.Collections.ObjectModel;
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
        private readonly IApplicationService _applicationService; // Сервис работы с заявками
        private readonly Application _application; // Редактируемая заявка
        private readonly Action? _onApplicationUpdated; // Callback для обновления списка

        [ObservableProperty]
        private ObservableCollection<ApplicationItem> _applicationItems = new(); // Позиции заявки

        [ObservableProperty]
        private string? _selectedStatus; // Выбранный статус заявки

        [ObservableProperty]
        private ObservableCollection<string> _availableStatuses = new() // Доступные статусы
        {
            "Черновик",
            "В обработке",
            "Ожидает оплаты",
            "В производстве",
            "Выполнена",
            "Отменена"
        };

        [ObservableProperty]
        private string _applicationNumber = "Неизвестно"; // Номер заявки

        [ObservableProperty]
        private string _partnerName = "Неизвестно"; // Наименование партнера

        [ObservableProperty]
        private DateTime _dateCreated = DateTime.Now; // Дата создания

        public IRelayCommand SaveCommand { get; } // Команда сохранения изменений
        public IRelayCommand CancelCommand { get; }  // Команда отмены редактировани

        public Window? CurrentWindow { get; set; } // Ссылка на текущее окно

        public EditApplicationViewModel(Application application, Action? onApplicationUpdated = null)
        {
            _application = application;
            _applicationService = new ApplicationService();
            _onApplicationUpdated = onApplicationUpdated;

            // Инициализация свойств данными из заявки
            ApplicationNumber = application.ApplicationNumber;
            PartnerName = application.PartnerName;
            DateCreated = application.DateCreated;
            SelectedStatus = application.Status;

            // Команда сохранения
            SaveCommand = new RelayCommand(async () => await SaveAsync());
            CancelCommand = new RelayCommand(() => Cancel()); // Команда отмены

            LoadApplicationItems(); // Загрузка позиций заявки
        }

        // Загрузка позиций (продуктов) заявки
        private async void LoadApplicationItems()
        {
            try
            {
                var items = await _applicationService.GetApplicationItemsAsync(_application.ApplicationId);
                ApplicationItems.Clear(); // Очистка текущего списка

                foreach (var item in items)
                {
                    if (item.Product != null)
                    {
                        item.ProductName = item.Product.Name; // Установка имени продукта
                        item.Price = item.Product.MinPricePartner ?? 0; // Установка цены
                    }
                    else
                    {
                        item.ProductName = "Неизвестный продукт";
                        item.Price = 0;
                    }

                    ApplicationItems.Add(item); // Добавление позиции в коллекцию
                }

                Console.WriteLine($"✅ Загружено {ApplicationItems.Count} позиций заявки");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка загрузки позиций заявки: {ex.Message}");
            }
        }

        // Сохранение изменений заявки
        private async Task SaveAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(SelectedStatus))
                {
                    _application.Status = SelectedStatus; // Обновление статуса
                    await _applicationService.UpdateApplicationAsync(_application); // Сохранение в БД
                    Console.WriteLine($"✅ Заявка {ApplicationNumber} обновлена. Новый статус: {SelectedStatus}");

                    // Вызываем callback для обновления списка заявок
                    _onApplicationUpdated?.Invoke();

                    CloseWindow(); // Закрытие окна
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка сохранения заявки: {ex.Message}");
            }
        }

        // Отмена редактирования
        private void Cancel()
        {
            Console.WriteLine("❌ Редактирование отменено");
            CloseWindow();
        }

        // Закрытие окна редактирования
        private void CloseWindow()
        {
            CurrentWindow?.Close();
        }
    }
}