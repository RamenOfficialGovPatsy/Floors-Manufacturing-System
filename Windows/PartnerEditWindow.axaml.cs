using Avalonia.Controls;
using Master_Floor_Project.ViewModels;
using System;

namespace Master_Floor_Project.Windows
{
    public partial class PartnerEditWindow : Window
    {
        public PartnerEditWindow()
        {
            InitializeComponent();
            this.DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object? sender, EventArgs e)
        {
            if (DataContext is PartnerEditViewModel viewModel)
            {
                viewModel.OnRequestClose += () => this.Close();
            }
        }
    }
}