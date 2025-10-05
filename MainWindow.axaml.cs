// УБЕДИТЕСЬ, ЧТО NAMESPACE И ИМЯ КЛАССА СОВПАДАЮТ С x:Class В ФАЙЛЕ ВЫШЕ
using Avalonia.Controls;
using Master_Floor_Project.ViewModels;

namespace Master_Floor_Project
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}