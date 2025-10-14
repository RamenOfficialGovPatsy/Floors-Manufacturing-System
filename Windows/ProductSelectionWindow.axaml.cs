using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using Master_Floor_Project.ViewModels;

namespace Master_Floor_Project.Windows
{
    public partial class ProductSelectionWindow : Window
    {
        public ProductSelectionWindow()
        {
            InitializeComponent();
            DataContext = new ProductSelectionViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void DeselectDataGrid_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (e.Source is not Visual source) return;

            var isClickOnRow = source.GetSelfAndVisualAncestors()
                .OfType<DataGridRow>()
                .Any();

            // Используем FindControl для доступа к DataGrid
            var productsDataGrid = this.FindControl<DataGrid>("ProductsDataGrid");
            if (!isClickOnRow && productsDataGrid != null)
            {
                productsDataGrid.SelectedItem = null;
            }
        }
    }
}