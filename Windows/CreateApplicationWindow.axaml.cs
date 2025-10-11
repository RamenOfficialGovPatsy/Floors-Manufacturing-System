using Avalonia.Controls;
using Master_Floor_Project.ViewModels;

namespace Master_Floor_Project.Windows
{
    public partial class CreateApplicationWindow : Window
    {
        public CreateApplicationWindow()
        {
            InitializeComponent();
            DataContext = new CreateApplicationViewModel();
        }
    }
}