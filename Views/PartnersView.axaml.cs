using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using Master_Floor_Project.ViewModels;
using System.Linq;

namespace Master_Floor_Project.Views
{
    public partial class PartnersView : UserControl
    {
        public PartnersView()
        {
            InitializeComponent();
            this.Loaded += PartnersView_Loaded;
        }

        private async void PartnersView_Loaded(object? sender, RoutedEventArgs e)
        {
            if (DataContext is PartnersViewModel viewModel)
            {
                await viewModel.LoadPartnersAsync();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.GetVisualRoot() is Window window)
            {
                window.Close();
            }
        }

        private void DeselectDataGrid_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (e.Source is not Visual source) return;
            var isClickOnRow = source.GetSelfAndVisualAncestors().OfType<DataGridRow>().Any();
            if (!isClickOnRow && this.DataContext is PartnersViewModel viewModel)
            {
                viewModel.SelectedPartner = null;
            }
        }
    }
}