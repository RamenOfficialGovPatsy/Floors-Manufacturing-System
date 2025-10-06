using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree; // <-- Этот using на месте
using Master_Floor_Project.ViewModels;

namespace Master_Floor_Project.Views
{
    public partial class PartnersView : UserControl
    {
        public PartnersView()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Parent is Window window)
            {
                window.Close();
            }
        }

        private void DeselectDataGrid_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            // Используем IVisual
            if (e.Source is not Visual source) return;

            // Используем SelfAndVisualAncestors
            var isClickOnRow = source.GetSelfAndVisualAncestors()
                .OfType<DataGridRow>()
                .Any();

            if (!isClickOnRow && this.DataContext is PartnersViewModel viewModel)
            {
                viewModel.SelectedPartner = null;
            }
        }
    }
}