using Avalonia.Controls;
using System;

namespace Master_Floor_Project.Services
{
    public static class NavigationService
    {
        public static void ShowWindow<T>() where T : Window
        {
            var window = Activator.CreateInstance<T>();
            window.Show();
        }
    }
}