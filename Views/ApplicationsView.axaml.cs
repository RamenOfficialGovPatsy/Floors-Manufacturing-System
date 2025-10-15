using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using System.Linq;

namespace Master_Floor_Project.Views
{
    public partial class ApplicationsView : UserControl
    {
        public ApplicationsView()
        {
            InitializeComponent(); // Инициализация XAML компонентов
        }

        // Обработчик нажатия кнопки "Назад" - закрытие текущего окна
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Parent is Window window) // Поиск родительского окна
            {
                window.Close(); // Закрытие окна
            }
        }

        // Обработчик нажатия для снятия выделения с DataGrid при клике на пустую область
        private void DeselectDataGrid_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            // Проверка что источник события - визуальный элемент
            if (e.Source is not Visual source) return;

            // Проверка был ли клик на строке DataGrid
            var isClickOnRow = source.GetSelfAndVisualAncestors()
                .OfType<DataGridRow>()
                .Any();

            // Если клик был не на строке - снимаем выделение
            if (!isClickOnRow && ApplicationsDataGrid != null) ApplicationsDataGrid.SelectedItem = null;
        }
    }
}