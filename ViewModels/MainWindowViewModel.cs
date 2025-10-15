using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Master_Floor_Project.Services;
using Master_Floor_Project.Windows;

namespace Master_Floor_Project.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        // Команды навигации к основным модулям приложения
        public ICommand NavigateToPartnersCommand { get; } // Переход к управлению партнерами
        public ICommand NavigateToProductsCommand { get; }  // Переход к каталогу продукции
        public ICommand NavigateToWarehouseCommand { get; } // Переход к складским остаткам
        public ICommand NavigateToApplicationsCommand { get; } // Переход к заявкам

        public MainWindowViewModel()
        {
            // Инициализация команд навигации - каждая команда открывает соответствующее окно
            NavigateToPartnersCommand = new RelayCommand(() => NavigationService.ShowWindow<PartnersWindow>());

            // Инициализация команды перехода к каталогу продукции  
            NavigateToProductsCommand = new RelayCommand(() => NavigationService.ShowWindow<ProductsWindow>());

            // Инициализация команды перехода к складским остаткам
            NavigateToWarehouseCommand = new RelayCommand(() => NavigationService.ShowWindow<WarehouseWindow>());

            // Инициализация команды перехода к управлению заявками
            NavigateToApplicationsCommand = new RelayCommand(() => NavigationService.ShowWindow<ApplicationsWindow>());
        }
    }
}