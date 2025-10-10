using Avalonia.Controls;
using Avalonia.Interactivity;
using Master_Floor_Project.ViewModels;
using System;
using System.Diagnostics;

namespace Master_Floor_Project.Views
{
    public partial class ProductsView : UserControl
    {
        public ProductsView()
        {
            InitializeComponent();

            // Устанавливаем DataContext если он не установлен
            if (DataContext == null)
            {
                DataContext = new ProductsViewModel();
                Debug.WriteLine("🟢 ProductsView: DataContext установлен принудительно");
            }
            else
            {
                Debug.WriteLine($"🟢 ProductsView: DataContext уже установлен: {DataContext.GetType().Name}");
            }

            this.Loaded += ProductsView_Loaded;
        }

        private async void ProductsView_Loaded(object? sender, RoutedEventArgs e)
        {
            Debug.WriteLine("🟡 ProductsView: Загружена форма продукции");

            if (DataContext is ProductsViewModel viewModel)
            {
                await viewModel.LoadProductsAsync();
            }
            else
            {
                Debug.WriteLine($"🔴 ProductsView: DataContext не является ProductsViewModel. Тип: {DataContext?.GetType().Name ?? "NULL"}");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Parent is Window window)
            {
                window.Close();
            }
        }
    }
}