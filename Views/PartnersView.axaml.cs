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
            this.Loaded += PartnersView_Loaded; // Подписка на событие загрузки UserControl
        }

        // Обработчик события загрузки UserControl - загрузка данных партнеров
        private async void PartnersView_Loaded(object? sender, RoutedEventArgs e)
        {
            // Проверка что DataContext правильного типа
            if (DataContext is PartnersViewModel viewModel)
            {
                // Загрузка списка партнеров из БД
                await viewModel.LoadPartnersAsync();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Получение корневого окна через визуальное дерево
            if (this.GetVisualRoot() is Window window)
            {
                window.Close();
            }
        }

        private void DeselectDataGrid_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (e.Source is not Visual source) return; // Проверка что источник события - визуальный элемент

            // Проверка был ли клик на строке DataGrid
            var isClickOnRow = source.GetSelfAndVisualAncestors().OfType<DataGridRow>().Any();

            // Если клик был не на строке - снимаем выделение через ViewModel
            if (!isClickOnRow && this.DataContext is PartnersViewModel viewModel)
            {
                viewModel.SelectedPartner = null;
            }
        }
    }
}