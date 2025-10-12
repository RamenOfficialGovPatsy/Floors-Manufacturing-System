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
            var window = new T();
            window.DataContext = viewModel;
            window.Show();
        }
    }
}