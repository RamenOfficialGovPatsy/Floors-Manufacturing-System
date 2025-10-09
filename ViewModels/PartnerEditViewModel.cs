// ViewModels/PartnerEditViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_Floor_Project.Models;
using Master_Floor_Project.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Master_Floor_Project.ViewModels
{
    public partial class PartnerEditViewModel : ViewModelBase
    {
        private readonly IPartnerService _partnerService;
        public event Action? OnRequestClose;

        [ObservableProperty]
        [Required(ErrorMessage = "Наименование компании обязательно")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _name = "";

        [ObservableProperty] private string? _type;
        [ObservableProperty] private string? _address;
        [ObservableProperty] private string? _inn;
        [ObservableProperty] private string? _directorName;
        [ObservableProperty] private string? _phone;
        [ObservableProperty] private string? _email;
        [ObservableProperty] private string? _rating;

        public PartnerEditViewModel()
        {
            _partnerService = new PartnerService();
            ValidateAllProperties();
        }

        private bool CanSave()
        {
            return !HasErrors;
        }

        [RelayCommand(CanExecute = nameof(CanSave))]
        private async Task SaveAsync()
        {
            ValidateAllProperties();
            if (HasErrors) return;

            try
            {
                // ИЗМЕНЕНИЕ 1: Добавляем безопасную обработку рейтинга
                int? partnerRating = null;
                if (!string.IsNullOrEmpty(this.Rating))
                {
                    // Считаем количество символов '⭐'
                    partnerRating = this.Rating.Length;
                }

                var newPartner = new Partner
                {
                    Name = this.Name,
                    Type = this.Type,
                    Address = this.Address,
                    Inn = this.Inn ?? string.Empty,
                    DirectorName = this.DirectorName,
                    Phone = this.Phone,
                    Email = this.Email,
                    Rating = partnerRating // ДОБАВЛЕНО: Сохраняем рейтинг
                };

                await _partnerService.AddPartnerAsync(newPartner);
                EventAggregator.PublishPartnerAdded();
                OnRequestClose?.Invoke();
            }
            catch (Exception ex)
            {
                // ИЗМЕНЕНИЕ 2: Улучшаем логирование для будущих ошибок
                Console.WriteLine($"Ошибка при сохранении партнера: {ex}");
            }
        }
    }
}