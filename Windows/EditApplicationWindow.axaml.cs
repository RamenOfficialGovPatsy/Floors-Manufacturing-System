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

        public EditApplicationWindow(EditApplicationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            this.Opened += (s, e) =>
            {
                if (DataContext is EditApplicationViewModel vm)
                {
                    vm.CurrentWindow = this;
                }
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}