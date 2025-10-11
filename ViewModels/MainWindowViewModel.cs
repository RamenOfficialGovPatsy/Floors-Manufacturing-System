using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Master_Floor_Project.Services;
using Master_Floor_Project.Windows;

namespace Master_Floor_Project.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        // Команды для навигации
        public ICommand NavigateToPartnersCommand { get; }
        public ICommand NavigateToProductsCommand { get; }
        public ICommand NavigateToWarehouseCommand { get; }
        public ICommand NavigateToApplicationsCommand { get; }

        public MainWindowViewModel()
        {
            // Инициализация команд. Каждая команда вызывает наш NavigationService
            NavigateToPartnersCommand = new RelayCommand(() => NavigationService.ShowWindow<PartnersWindow>());
            NavigateToProductsCommand = new RelayCommand(() => NavigationService.ShowWindow<ProductsWindow>());
            NavigateToWarehouseCommand = new RelayCommand(() => NavigationService.ShowWindow<WarehouseWindow>());
            NavigateToApplicationsCommand = new RelayCommand(() => NavigationService.ShowWindow<ApplicationsWindow>());
        }
    }
}