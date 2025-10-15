using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Master_Floor_Project
{
    public partial class App : Application
    {
        // Загрузка XAML разметки приложения
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        // Настройка приложения после инициализации: тема и главное окно
        public override void OnFrameworkInitializationCompleted()
        {
            // Устанавливаем светлую тему для всего приложения
            this.RequestedThemeVariant = ThemeVariant.Light;

            // Создаем главное окно для десктопного приложения
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}