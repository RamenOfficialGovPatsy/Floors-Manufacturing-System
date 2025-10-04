using Avalonia.Controls;
using Avalonia.Interactivity;

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
            // Закрываем только текущее окно партнеров
            if (this.Parent is Window window)
            {
                window.Close();
            }

            // Главное окно остается открытым
        }
    }
}