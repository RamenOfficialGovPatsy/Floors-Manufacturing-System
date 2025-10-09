using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Master_Floor_Project.Views

{
    public partial class PartnerEditView : UserControl
    {
        public PartnerEditView()
        {
            InitializeComponent();
        }


        private void Deselect_OnPointerPressed(object? sender, PointerPressedEventArgs e)

        {
            if (e.Source is Border)
            {
                var topLevel = TopLevel.GetTopLevel(this);
                topLevel?.FocusManager?.ClearFocus();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрываем текущее окно
            if (this.Parent is Window window)
            {
                window.Close();
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрываем окно добавления партнера (то же что и НАЗАД)
            if (this.Parent is Window window)
            {
                window.Close();
            }
        }
    }
}