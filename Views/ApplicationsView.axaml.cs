using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Master_Floor_Project.Views
{
    public partial class ApplicationsView : UserControl
    {
        public ApplicationsView()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрываем текущее окно
            if (this.Parent is Window window)
            {
                window.Close();
            }
        }
    }
}