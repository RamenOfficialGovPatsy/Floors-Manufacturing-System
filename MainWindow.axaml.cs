using Avalonia.Controls;
using Master_Floor_Project.ViewModels;

namespace Master_Floor_Project
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Установка ViewModel для привязки данных в XAML
            DataContext = new MainWindowViewModel();
        }
    }
}