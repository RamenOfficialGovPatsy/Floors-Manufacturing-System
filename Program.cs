using Avalonia;
using System;

namespace Master_Floor_Project
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Конфигурация Avalonia приложения с кроссплатформенными настройками
        public static AppBuilder BuildAvaloniaApp()
        {
            // Конфигурация и возврат AppBuilder для запуска приложения
            return AppBuilder.Configure<App>()
                .UsePlatformDetect() // Автоопределение платформы
                .WithInterFont() // Шрифт Inter
                .LogToTrace() // Логирование в трассировку
                .UseX11()       // Поддержка X11 для Linux
                .UseSkia();     // Графический бэкенд Skia для кроссплатформенности
        }
    }
}