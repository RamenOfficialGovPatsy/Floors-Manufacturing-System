using Avalonia.Controls;
using Master_Floor_Project.ViewModels;

namespace Master_Floor_Project.Windows
{
    public partial class ProductsWindow : Window
    {
        public ProductsWindow()
        {
            InitializeComponent();
            DataContext = new ProductsViewModel();
        }
    }
}