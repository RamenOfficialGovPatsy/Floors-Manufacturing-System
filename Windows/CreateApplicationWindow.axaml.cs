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

        public CreateApplicationWindow(CreateApplicationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            this.Opened += (s, e) =>
            {
                if (DataContext is CreateApplicationViewModel vm)
                {
                    vm.CurrentWindow = this;
                }
            };
        }
    }
}