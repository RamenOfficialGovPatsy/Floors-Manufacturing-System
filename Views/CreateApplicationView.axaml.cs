using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using System;
using System.Linq;

namespace Master_Floor_Project.Views
{
    public partial class CreateApplicationView : UserControl
    {
        public CreateApplicationView()
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
            if (e.Source is not Visual source) return;

            var isClickOnRow = source.GetSelfAndVisualAncestors()
                .OfType<DataGridRow>()
                .Any();

            if (!isClickOnRow && ItemsDataGrid != null)
            {
                ItemsDataGrid.SelectedItem = null;
            }
        }
    }
}