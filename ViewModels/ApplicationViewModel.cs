using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_Floor_Project.Models;
using Master_Floor_Project.Services;
using Master_Floor_Project.Windows;
using System;
using System.Threading.Tasks;

namespace Master_Floor_Project.ViewModels
{
    public partial class ApplicationViewModel : ViewModelBase
    {
        // Сервис для работы с заявками
        private readonly IApplicationService _applicationService;

        [ObservableProperty]
        private ObservableCollection<Application> _applications = new(); // Коллекция заявок для отображения

        [ObservableProperty]
        private bool _isLoading = true; // Флаг загрузки данных

        [ObservableProperty]
        private Application? _selectedApplication; // Выбранная заявка в списке

        public ICommand CreateApplicationCommand { get; } // Команда создания новой заявки
        public ICommand RefreshApplicationsCommand { get; } // Команда обновления списка заявок
        public ICommand EditApplicationCommand { get; } // Команда редактирования заявки
        public ICommand DeleteApplicationCommand { get; } // Команда удаления заявки

        public ApplicationViewModel()
        {
            _applicationService = new ApplicationService();

            // Инициализация команды создания заявки
            CreateApplicationCommand = new RelayCommand(() =>
            {
                Console.WriteLine("Кнопка СОЗДАТЬ ЗАЯВКУ нажата");
                ShowCreateWindow();
            });

            // Инициализация команды обновления списка
            RefreshApplicationsCommand = new RelayCommand(async () => await LoadApplicationsAsync());

            // Инициализация команды редактирования заявки
            EditApplicationCommand = new RelayCommand(() =>
            {
                if (SelectedApplication != null)
                {
                    Console.WriteLine($"Редактирование заявки ID: {SelectedApplication.ApplicationId}");
                    ShowEditWindow(SelectedApplication);
                }
                else
                {
                    Console.WriteLine("Не выбрана заявка для редактирования");
                }
            });

            // Инициализация команды удаления заявки
            DeleteApplicationCommand = new RelayCommand(async () =>
            {
                if (SelectedApplication != null)
                {
                    Console.WriteLine($"Удаление заявки ID: {SelectedApplication.ApplicationId}");
                    await DeleteApplicationAsync(SelectedApplication);
                }
                else
                {
                    Console.WriteLine("Не выбрана заявка для удаления");
                }
            });

            // Загружаем данные при инициализации
            _ = LoadApplicationsAsync();
        }

        // Открытие окна редактирования заявки
        private void ShowEditWindow(Application application)
        {
            Console.WriteLine($"Открытие редактирования заявки {application.ApplicationNumber}");

            // Создаем ViewModel с callback для обновления списка
            var editViewModel = new EditApplicationViewModel(application, onApplicationUpdated: () =>
            {
                // Этот код выполнится после сохранения заявки
                Console.WriteLine("🔄 Обновляем список заявок после редактирования...");
                _ = LoadApplicationsAsync(); // Перезагружаем список
            });

            NavigationService.ShowWindow<EditApplicationWindow, EditApplicationViewModel>(editViewModel);
        }

        // Открытие окна создания новой заявки
        private void ShowCreateWindow()
        {
            Console.WriteLine("Кнопка СОЗДАТЬ ЗАЯВКУ нажата");

            var createViewModel = new CreateApplicationViewModel();
            createViewModel.OnApplicationCreated += () =>
            {
                // Этот код выполнится после создания заявки
                Console.WriteLine("🔄 Обновляем список заявок после создания...");
                _ = LoadApplicationsAsync(); // Перезагружаем список
            };

            NavigationService.ShowWindow<CreateApplicationWindow, CreateApplicationViewModel>(createViewModel);
        }

        // Удаление выбранной заявки
        private async Task DeleteApplicationAsync(Application application)
        {
            try
            {
                await _applicationService.DeleteApplicationAsync(application.ApplicationId);
                Console.WriteLine($"Заявка {application.ApplicationNumber} успешно удалена");

                // Обновляем список заявок после удаления
                await LoadApplicationsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка удаления заявки: {ex.Message}");
            }
        }

        // Загрузка списка заявок из базы данных
        private async Task LoadApplicationsAsync()
        {
            try
            {
                IsLoading = true; // Включение индикатора загрузки
                Applications.Clear(); // Очистка текущего списка

                // Получение заявок из БД
                var applications = await _applicationService.GetApplicationsAsync();
                foreach (var application in applications)
                {
                    Applications.Add(application); // Добавление заявок в коллекцию
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки заявок: {ex.Message}");
            }
            finally
            {
                IsLoading = false; // Выключение индикатора загрузки
            }
        }
    }
}