using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Master_Floor_Project.ViewModels;

namespace Master_Floor_Project.Windows
{
    public partial class EditApplicationWindow : Window
    {
        public EditApplicationWindow()
        {
            InitializeComponent();
        }

        // Конструктор с передачей ViewModel - используется при открытии окна из кода
        public EditApplicationWindow(EditApplicationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            // Подписка на событие открытия окна для установки ссылки на окно в ViewModel
            this.Opened += (s, e) =>
            {
                if (DataContext is EditApplicationViewModel vm)
                {
                    vm.CurrentWindow = this; // Передача ссылки на текущее окно в ViewModel
                }
            };
        }

        private void InitializeComponent()
        {
            // Загрузка XAML разметки из файла .axam
            AvaloniaXamlLoader.Load(this);
        }
    }
}