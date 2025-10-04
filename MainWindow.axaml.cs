using Avalonia.Controls;
using Avalonia.Interactivity;
using Master_Floor_Project.Views;

namespace Master_Floor_Project
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PartnersButton_Click(object sender, RoutedEventArgs e)
        {
            // Создаем и показываем окно партнеров
            var partnersWindow = new PartnersWindow();
            partnersWindow.Show();
        }
    }
}