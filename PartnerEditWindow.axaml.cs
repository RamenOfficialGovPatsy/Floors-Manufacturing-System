// PartnerEditWindow.axaml.cs
using Avalonia.Controls;
using Master_Floor_Project.ViewModels;
using System; // Добавляем этот using

namespace Master_Floor_Project
{
    public partial class PartnerEditWindow : Window
    {
        public PartnerEditWindow()
        {
            InitializeComponent();
            // Подписываемся на событие изменения DataContext
            this.DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object? sender, EventArgs e)
        {
            // Теперь, когда DataContext точно назначен, мы можем
            // безопасно подписаться на событие
            if (DataContext is PartnerEditViewModel viewModel)
            {
                viewModel.OnRequestClose += () => this.Close();
            }
        }
    }
}