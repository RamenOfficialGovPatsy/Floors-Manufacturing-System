using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_Floor_Project.Models;
using Master_Floor_Project.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Master_Floor_Project.ViewModels
{
    public partial class PartnerEditViewModel : ViewModelBase
    {
        private readonly IPartnerService _partnerService;
        public event Action? OnRequestClose;
        private int? _editingPartnerId;

        [ObservableProperty]
        [Required(ErrorMessage = "Наименование компании обязательно")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _name = "";

        [ObservableProperty]
        [RegularExpression(@"^(\d{10}|\d{12})$", ErrorMessage = "ИНН должен состоять из 10 или 12 цифр")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string? _inn;

        [ObservableProperty]
        [EmailAddress(ErrorMessage = "Некорректный формат Email адреса")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string? _email;

        [ObservableProperty] private string? _type;
        [ObservableProperty] private string? _address;
        [ObservableProperty] private string? _directorName;
        [ObservableProperty] private string? _phone;
        [ObservableProperty] private string? _rating;

        public PartnerEditViewModel()
        {
            _partnerService = new PartnerService();
            ValidateAllProperties();
        }

        public void LoadPartner(Partner partner)
        {
            _editingPartnerId = partner.PartnerId;
            Name = partner.Name;
            Type = partner.Type;
            Address = partner.Address;
            Inn = partner.Inn;
            DirectorName = partner.DirectorName;
            Phone = partner.Phone;
            Email = partner.Email;
            Rating = partner.Rating.HasValue ? new string('⭐', partner.Rating.Value) : null;
        }

        private bool CanSave() => !HasErrors;

        [RelayCommand(CanExecute = nameof(CanSave))]
        private async Task SaveAsync()
        {
            ValidateAllProperties();
            if (HasErrors) return;

            try
            {
                int? partnerRating = !string.IsNullOrEmpty(this.Rating) ? this.Rating.Length : null;
                var partner = new Partner
                {
                    Name = this.Name,
                    Type = this.Type,
                    Address = this.Address,
                    Inn = this.Inn ?? string.Empty,
                    DirectorName = this.DirectorName,
                    Phone = this.Phone,
                    Email = this.Email,
                    Rating = partnerRating
                };

                if (_editingPartnerId.HasValue)
                {
                    partner.PartnerId = _editingPartnerId.Value;
                    await _partnerService.UpdatePartnerAsync(partner);
                }
                else
                {
                    await _partnerService.AddPartnerAsync(partner);
                }

                EventAggregator.PublishPartnersChanged();
                OnRequestClose?.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении партнера: {ex}");
            }
        }
    }
}