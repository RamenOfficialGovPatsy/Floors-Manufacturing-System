using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using System.Linq;

namespace Master_Floor_Project.Views
{
    // UserControl для создания новой заявки
    public partial class CreateApplicationView : UserControl
    {
        public CreateApplicationView()
        {
            InitializeComponent();
        }

        // Обработчик нажатия кнопки "Назад" - закрытие окна создания заявки
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Parent is Window window)
            {
                window.Close();
            }
        }

        // Обработчик для снятия выделения с DataGrid продуктов при клике на пустую область
        private void DeselectDataGrid_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (e.Source is not Visual source) return; // Проверка что источник события - визуальный элемент

            // Проверка был ли клик на строке DataGrid
            var isClickOnRow = source.GetSelfAndVisualAncestors()
                .OfType<DataGridRow>()
                .Any();

            // Если клик был не на строке - снимаем выделение с DataGrid продуктов
            if (!isClickOnRow && ItemsDataGrid != null)
            {
                ItemsDataGrid.SelectedItem = null;
            }
        }
    }
}