using Avalonia.Controls;
using Master_Floor_Project.ViewModels;

namespace Master_Floor_Project
{
    public partial class ApplicationsWindow : Window
    {
        public ApplicationsWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel();
        }
    }
}