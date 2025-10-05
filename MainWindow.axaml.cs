using Avalonia.Controls;
using Avalonia.Interactivity;

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
            var partnersWindow = new PartnersWindow();
            partnersWindow.Show();
        }

        private void ProductsButton_Click(object sender, RoutedEventArgs e)
        {
            var productsWindow = new ProductsWindow();
            productsWindow.Show();
        }

        // ДОБАВЬТЕ ЭТОТ МЕТОД
        private void WarehouseButton_Click(object sender, RoutedEventArgs e)
        {
            var warehouseWindow = new WarehouseWindow();
            warehouseWindow.Show();
        }

        private void ApplicationsButton_Click(object sender, RoutedEventArgs e)
        {
            var applicationsWindow = new ApplicationsWindow();
            applicationsWindow.Show();
        }
    }
}