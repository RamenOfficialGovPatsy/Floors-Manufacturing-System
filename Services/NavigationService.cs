using Avalonia.Controls;
using System;

namespace Master_Floor_Project.Services
{
    public static class NavigationService
    {
        public static void ShowWindow<T>() where T : Window, new()
        {
            var window = new T();
            window.Show();
        }

        public static void ShowWindow<T, TViewModel>(TViewModel viewModel)
            where T : Window, new()
            where TViewModel : class
        {
            try
            {
                var constructor = typeof(T).GetConstructor(new[] { typeof(TViewModel) });
                Window window;

                if (constructor != null)
                {
                    window = (Window)constructor.Invoke(new object[] { viewModel });
                }
                else
                {
                    window = new T();
                    window.DataContext = viewModel;
                }
                window.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ NavigationService Error: {ex.Message}");
                throw;
            }
        }
    }
}