using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_Floor_Project.Models;
using Master_Floor_Project.Services;

namespace Master_Floor_Project.ViewModels
{
    public partial class ApplicationViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<Application> _applications = new();

        public ICommand CreateApplicationCommand { get; }

        public ApplicationViewModel()
        {
            CreateApplicationCommand = new RelayCommand(() => NavigationService.ShowWindow<CreateApplicationWindow>());
            LoadApplications();
        }

        private void LoadApplications()
        {
            // Используем тестовые данные, как и раньше
            Applications.Add(new Application { ApplicationNumber = "Z-2025-001", PartnerName = "ООО \"Вектор\"", DateCreated = new DateTime(2025, 1, 15), Status = "В обработке" });
            Applications.Add(new Application { ApplicationNumber = "Z-2025-002", PartnerName = "ООО \"Стройка\"", DateCreated = new DateTime(2025, 1, 16), Status = "Выполнена" });
            Applications.Add(new Application { ApplicationNumber = "Z-2025-003", PartnerName = "ИП Сидоров А.В.", DateCreated = new DateTime(2025, 1, 17), Status = "Новая" });
        }
    }
}