using Avalonia.Controls;
using Avalonia.Interactivity;
using Master_Floor_Project.ViewModels;
using System.Diagnostics;

namespace Master_Floor_Project.Views
{
    public partial class ProductsView : UserControl
    {
        public ProductsView()
        {
            InitializeComponent();

            // Установка DataContext если он не был установлен извне
            if (DataContext == null)
            {
                // Создание и установка ViewModel
                DataContext = new ProductsViewModel();
                Debug.WriteLine("🟢 ProductsView: DataContext установлен принудительно");
            }
            else
            {
                Debug.WriteLine($"🟢 ProductsView: DataContext уже установлен: {DataContext.GetType().Name}");
            }

            // Подписка на событие загрузки UserControl
            this.Loaded += ProductsView_Loaded;
        }

        // Обработчик события загрузки UserControl - загрузка данных продукции
        private async void ProductsView_Loaded(object? sender, RoutedEventArgs e)
        {
            Debug.WriteLine("🟡 ProductsView: Загружена форма продукции");

            // Проверка что DataContext правильного типа
            if (DataContext is ProductsViewModel viewModel)
            {
                // Загрузка списка продуктов из БД
                await viewModel.LoadProductsAsync();
            }
            else
            {
                Debug.WriteLine($"🔴 ProductsView: DataContext не является ProductsViewModel. Тип: {DataContext?.GetType().Name ?? "NULL"}");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Parent is Window window) // Поиск родительского окна
            {
                window.Close();
            }
        }
    }
}