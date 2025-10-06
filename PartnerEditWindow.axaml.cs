using Avalonia.Controls;
using Master_Floor_Project.ViewModels;

namespace Master_Floor_Project
{
    public partial class PartnerEditWindow : Window
    {
        public PartnerEditWindow()
        {
            InitializeComponent();
            DataContext = new PartnerEditViewModel();
        }
    }
}