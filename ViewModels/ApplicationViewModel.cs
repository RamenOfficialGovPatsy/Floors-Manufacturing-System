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
        private readonly IApplicationService _applicationService;

        [ObservableProperty]
        private ObservableCollection<Application> _applications = new();

        [ObservableProperty]
        private bool _isLoading = true;

        [ObservableProperty]
        private Application? _selectedApplication;

        public ICommand CreateApplicationCommand { get; }
        public ICommand RefreshApplicationsCommand { get; }
        public ICommand EditApplicationCommand { get; }
        public ICommand DeleteApplicationCommand { get; }

        public ApplicationViewModel()
        {
            _applicationService = new ApplicationService();

            CreateApplicationCommand = new RelayCommand(() =>
            {
                Console.WriteLine("Кнопка СОЗДАТЬ ЗАЯВКУ нажата");
                NavigationService.ShowWindow<CreateApplicationWindow>();
            });

            RefreshApplicationsCommand = new RelayCommand(async () => await LoadApplicationsAsync());

            EditApplicationCommand = new RelayCommand(() =>
            {
                if (SelectedApplication != null)
                {
                    Console.WriteLine($"Редактирование заявки ID: {SelectedApplication.ApplicationId}");
                    // TODO: Открыть окно редактирования заявки
                    ShowEditWindow(SelectedApplication);
                }
                else
                {
                    Console.WriteLine("Не выбрана заявка для редактирования");
                }
            });

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

        private void ShowEditWindow(Application application)
        {
            Console.WriteLine($"Открытие редактирования заявки {application.ApplicationNumber}");

            // ✅ Создаем ViewModel с callback для обновления списка
            var editViewModel = new EditApplicationViewModel(application, onApplicationUpdated: () =>
            {
                // ✅ Этот код выполнится после сохранения заявки
                Console.WriteLine("🔄 Обновляем список заявок после редактирования...");
                _ = LoadApplicationsAsync(); // Перезагружаем список
            });

            NavigationService.ShowWindow<EditApplicationWindow, EditApplicationViewModel>(editViewModel);
        }

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

        private async Task LoadApplicationsAsync()
        {
            try
            {
                IsLoading = true;
                Applications.Clear();

                var applications = await _applicationService.GetApplicationsAsync();
                foreach (var application in applications)
                {
                    Applications.Add(application);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки заявок: {ex.Message}");
                // Можно добавить заглушки или сообщение об ошибке
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}