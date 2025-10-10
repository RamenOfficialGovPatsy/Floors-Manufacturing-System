using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_Floor_Project.Models;
using Master_Floor_Project.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Master_Floor_Project.ViewModels
{
    public partial class PartnersViewModel : ViewModelBase
    {
        private readonly IPartnerService _partnerService;

        [ObservableProperty] private ObservableCollection<Partner> _partners;
        [ObservableProperty][NotifyCanExecuteChangedFor(nameof(EditPartnerCommand))][NotifyCanExecuteChangedFor(nameof(DeletePartnerCommand))] private Partner? _selectedPartner;
        [ObservableProperty] private bool _isLoading = true;

        public PartnersViewModel()
        {
            _partnerService = new PartnerService();
            Partners = new ObservableCollection<Partner>();
            EventAggregator.PartnersChanged += OnPartnersChanged;
        }

        private async void OnPartnersChanged()
        {
            await LoadPartnersAsync();
        }

        public async Task LoadPartnersAsync()
        {
            IsLoading = true;
            try
            {
                Partners.Clear();
                var partnersList = await _partnerService.GetPartnersAsync();
                foreach (var partner in partnersList)
                {
                    Partners.Add(partner);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке партнеров: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private void AddPartner()
        {
            var editViewModel = new PartnerEditViewModel();
            var editWindow = new PartnerEditWindow { DataContext = editViewModel };
            editWindow.Show();
        }

        [RelayCommand(CanExecute = nameof(CanEditOrDeletePartner))]
        private void EditPartner(Partner? partner)
        {
            if (partner is null) return;
            var editViewModel = new PartnerEditViewModel();
            editViewModel.LoadPartner(partner);
            var editWindow = new PartnerEditWindow { DataContext = editViewModel };
            editWindow.Show();
        }

        [RelayCommand(CanExecute = nameof(CanEditOrDeletePartner))]
        private async Task DeletePartnerAsync(Partner? partner)
        {
            if (partner is null) return;
            try
            {
                await _partnerService.DeletePartnerAsync(partner.PartnerId);
                await LoadPartnersAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении партнера: {ex.Message}");
                Debug.WriteLine($"Inner exception: {ex.InnerException?.Message}");
            }
        }

        private bool CanEditOrDeletePartner() => SelectedPartner != null;
    }
}