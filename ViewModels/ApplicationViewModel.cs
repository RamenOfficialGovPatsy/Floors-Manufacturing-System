using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_Floor_Project.Models;
// using Master_Floor_Project.Services; // Services пока не используются, можно закомментировать

namespace Master_Floor_Project.ViewModels
{
    public partial class ApplicationViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<Application> _applications = new();

        // NavigationService пока не используется, можно убрать
        // public ICommand CreateApplicationCommand { get; }

        public ApplicationViewModel()
        {
            // CreateApplicationCommand = new RelayCommand(() => NavigationService.ShowWindow<CreateApplicationWindow>());
            LoadApplications();
        }

        private void LoadApplications()
        {
            // Очищаем коллекцию перед добавлением новых данных
            Applications.Clear();

            // Создаем тестовые данные с использованием новых, правильных полей
            Applications.Add(new Application { ApplicationId = 1, PartnerId = 1, DateCreated = new DateTime(2025, 1, 15), Status = "В обработке" });
            Applications.Add(new Application { ApplicationId = 2, PartnerId = 2, DateCreated = new DateTime(2025, 1, 16), Status = "Выполнена" });
            Applications.Add(new Application { ApplicationId = 3, PartnerId = 3, DateCreated = new DateTime(2025, 1, 17), Status = "Новая" });
        }
    }
}