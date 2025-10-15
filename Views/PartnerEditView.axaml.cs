using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Master_Floor_Project.Views
{
    // UserControl для добавления и редактирования партнеров
    public partial class PartnerEditView : UserControl
    {
        public PartnerEditView()
        {
            InitializeComponent();
        }

        // Обработчик для снятия фокуса с элементов при клике на пустую область (Border)
        private void Deselect_OnPointerPressed(object? sender, PointerPressedEventArgs e)

        {
            if (e.Source is Border) // Проверка что клик был на Border элементе
            {
                // Получение верхнеуровневого элемента
                var topLevel = TopLevel.GetTopLevel(this);

                // Снятие фокуса со всех элементов
                topLevel?.FocusManager?.ClearFocus();
            }
        }

        // Обработчик нажатия кнопки "Назад" - закрытие окна редактирования
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Parent is Window window) // Поиск родительского окна
            {
                window.Close();
            }
        }

        // Обработчик нажатия кнопки "Отмена" - закрытие окна без сохранения
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Parent is Window window) // Поиск родительского окна
            {
                window.Close();
            }
        }
    }
}