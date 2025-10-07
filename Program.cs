using Avalonia;
using System;

namespace Master_Floor_Project
{
    internal class Program
    {
        // Точка входа в приложение. Этот метод остается без изменений.
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Конфигурация Avalonia. Вот где должен быть ваш код.
        public static AppBuilder BuildAvaloniaApp()
        {
            // Мы возвращаем сконфигурированный AppBuilder
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace()
                .UseX11()       // <-- Строка для стабильности на Linux
                .UseSkia();     // <-- Вторая строка для стабильности на Linux
        }
    }
}