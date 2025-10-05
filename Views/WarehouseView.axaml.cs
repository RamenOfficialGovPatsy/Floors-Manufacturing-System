using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Master_Floor_Project.Views
{
    public partial class WarehouseView : UserControl
    {
        public WarehouseView()
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
    }
}