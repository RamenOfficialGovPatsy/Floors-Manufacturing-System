using System;
using System.Diagnostics;
using Avalonia.Controls;

namespace Master_Floor_Project.Services
{
    public static class NavigationService
    {
        // Открытие окна без передачи ViewModel
        public static void ShowWindow<T>() where T : Window, new()
        {
            var window = new T(); // Создание экземпляра окна
            window.Show(); // Отображение окна
        }

        // Открытие окна с передачей ViewModel через конструктор или DataContext
        public static void ShowWindow<T, TViewModel>(TViewModel viewModel)
            where T : Window, new()
            where TViewModel : class
        {
            try
            {
                // Поиск конструктора с параметром
                var constructor = typeof(T).GetConstructor(new[] { typeof(TViewModel) });
                Window window;

                if (constructor != null)
                {
                    // Создание через конструктор
                    window = (Window)constructor.Invoke(new object[] { viewModel });
                }
                else
                {
                    window = new T(); // Создание через конструктор по умолчанию
                    window.DataContext = viewModel; // Установка DataContext
                }
                window.Show(); // Отображение окна
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ NavigationService Error: {ex.Message}");
                throw;
            }
        }
    }
}