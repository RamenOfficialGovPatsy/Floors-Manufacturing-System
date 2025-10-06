using System.Collections.ObjectModel;
using System.Threading.Tasks; // Добавлено
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_Floor_Project.Models;
using Master_Floor_Project.Services;

namespace Master_Floor_Project.ViewModels
{
    public partial class PartnersViewModel : ViewModelBase
    {
        // ИНЖЕКЦИЯ СЕРВИСА
        private readonly IPartnerService _partnerService;

        [ObservableProperty]
        private ObservableCollection<Partner> _partners = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EditPartnerCommand))]
        [NotifyCanExecuteChangedFor(nameof(DeletePartnerCommand))]
        private Partner? _selectedPartner;

        public PartnersViewModel()
        {
            // СОЗДАЕМ ЭКЗЕМПЛЯР НАШЕГО СЕРВИСА
            _partnerService = new PartnerService();

            // Загружаем партнеров при запуске
            _ = LoadPartnersAsync();
        }

        // МЕТОД СТАЛ АСИНХРОННЫМ И ИСПОЛЬЗУЕТ СЕРВИС
        private async Task LoadPartnersAsync()
        {
            var partnersFromService = await _partnerService.GetPartnersAsync();

            Partners.Clear();
            foreach (var partner in partnersFromService)
            {
                Partners.Add(partner);
            }
        }

        [RelayCommand]
        private void AddPartner() => NavigationService.ShowWindow<PartnerEditWindow>();

        private bool CanEditOrDeletePartner() => SelectedPartner != null;

        [RelayCommand(CanExecute = nameof(CanEditOrDeletePartner))]
        private void EditPartner() => NavigationService.ShowWindow<PartnerEditWindow>();

        [RelayCommand(CanExecute = nameof(CanEditOrDeletePartner))]
        private void DeletePartner()
        {
            if (SelectedPartner != null)
            {
                Partners.Remove(SelectedPartner);
            }
        }
    }
}