using Avalonia.Controls;
using Master_Floor_Project.ViewModels;

namespace Master_Floor_Project.Windows
{
    public partial class EditApplicationWindow : Window
    {
        public EditApplicationWindow()
        {
            InitializeComponent();

            // ✅ Устанавливаем ссылку на окно в ViewModel
            this.Opened += (s, e) =>
            {
                if (DataContext is EditApplicationViewModel viewModel)
                {
                    viewModel.CurrentWindow = this;
                }
            };
        }
    }
}