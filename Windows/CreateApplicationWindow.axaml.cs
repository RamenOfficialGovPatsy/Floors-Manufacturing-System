using Avalonia.Controls;
using Master_Floor_Project.ViewModels;

namespace Master_Floor_Project.Windows
{
    public partial class CreateApplicationWindow : Window
    {
        public CreateApplicationWindow()
        {
            InitializeComponent();
        }

        // Конструктор с передачей ViewModel - используется при открытии окна из кода
        public CreateApplicationWindow(CreateApplicationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel; // Установка контекста данных для привязок

            // Подписка на событие открытия окна для установки ссылки на окно в ViewModel
            this.Opened += (s, e) =>
            {
                if (DataContext is CreateApplicationViewModel vm)
                {
                    vm.CurrentWindow = this; // Передача ссылки на текущее окно в ViewModel
                }
            };
        }
    }
}