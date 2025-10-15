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

            // Создание и установка ViewModel для выбора продуктов
            DataContext = new ProductSelectionViewModel();
        }

        private void InitializeComponent()
        {
            // Загрузка XAML разметки из файла .axaml
            AvaloniaXamlLoader.Load(this);
        }

        // Обработчик для снятия выделения с DataGrid при клике на пустую область
        private void DeselectDataGrid_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            // Проверка что источник события - визуальный элемент
            if (e.Source is not Visual source) return;

            // Проверка был ли клик на строке DataGrid
            var isClickOnRow = source.GetSelfAndVisualAncestors()
                .OfType<DataGridRow>()
                .Any();

            // Поиск DataGrid по имени в визуальном дереве
            var productsDataGrid = this.FindControl<DataGrid>("ProductsDataGrid");

            // Если клик был не на строке и DataGrid найден - снимаем выделение
            if (!isClickOnRow && productsDataGrid != null)
            {
                // Сброс выбранного элемента
                productsDataGrid.SelectedItem = null;
            }
        }
    }
}