using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_Floor_Project.Models;
using Master_Floor_Project.Services;

namespace Master_Floor_Project.ViewModels
{
    public partial class PartnersViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<Partner> _partners = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EditPartnerCommand))]
        [NotifyCanExecuteChangedFor(nameof(DeletePartnerCommand))]
        private Partner? _selectedPartner;

        public PartnersViewModel()
        {
            LoadPartners();
        }

        private void LoadPartners()
        {
            Partners.Add(new Partner { Name = "ООО \"Вектор\"", Inn = "1234567890", DirectorName = "Иванов И.И.", Phone = "+79991234567", Email = "vector@mail.com" });
            Partners.Add(new Partner { Name = "ООО \"Стройка\"", Inn = "0987654321", DirectorName = "Петров П.П.", Phone = "+79997654321", Email = "stroika@mail.com" });
            Partners.Add(new Partner { Name = "ИП Сидоров А.В.", Inn = "5554443331", DirectorName = "Сидоров А.В.", Phone = "+79995554433", Email = "sidorov@mail.com" });
        }

        // ИЗМЕНЕНИЕ: Используем свойство 'SelectedPartner' вместо поля '_selectedPartner'
        private bool CanEditOrDeletePartner() => SelectedPartner != null;

        [RelayCommand]
        private void AddPartner()
        {
            NavigationService.ShowWindow<PartnerEditWindow>();
        }

        [RelayCommand(CanExecute = nameof(CanEditOrDeletePartner))]
        private void EditPartner()
        {
            NavigationService.ShowWindow<PartnerEditWindow>();
        }

        [RelayCommand(CanExecute = nameof(CanEditOrDeletePartner))]
        private void DeletePartner()
        {
            // ИЗМЕНЕНИЕ: Используем свойство 'SelectedPartner' вместо поля '_selectedPartner'
            if (SelectedPartner != null)
            {
                Partners.Remove(SelectedPartner);
            }
        }
    }
}