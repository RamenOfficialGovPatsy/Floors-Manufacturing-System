using Avalonia.Controls;
using Avalonia.Interactivity;
using Master_Floor_Project.ViewModels;
using System;
using System.Diagnostics;

namespace Master_Floor_Project.Views
{
    public partial class WarehouseView : UserControl
    {
        public WarehouseView()
        {
            try
            {
                InitializeComponent();
                Debug.WriteLine("🟡 WarehouseView: Конструктор вызван");

                // Устанавливаем DataContext
                DataContext = new WarehouseViewModel();
                Debug.WriteLine("🟢 WarehouseView: DataContext установлен");

                this.Loaded += WarehouseView_Loaded;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 WarehouseView: Ошибка в конструкторе: {ex.Message}");
            }
        }

        // Обработчик события загрузки UserControl - загрузка данных склада
        private async void WarehouseView_Loaded(object? sender, RoutedEventArgs e)
        {
            try
            {
                Debug.WriteLine("🟡 WarehouseView: Загружена форма склада");

                // Проверка что DataContext правильного типа
                if (DataContext is WarehouseViewModel viewModel)
                {
                    // Загрузка складских остатков из БД
                    await viewModel.LoadWarehouseDataAsync();
                }
                else
                {
                    Debug.WriteLine($"🔴 WarehouseView: DataContext не является WarehouseViewModel");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 WarehouseView: Ошибка в Loaded: {ex.Message}");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.Parent is Window window) // Поиск родительского окна
                {
                    window.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"🔴 WarehouseView: Ошибка в BackButton_Click: {ex.Message}");
            }
        }
    }
}