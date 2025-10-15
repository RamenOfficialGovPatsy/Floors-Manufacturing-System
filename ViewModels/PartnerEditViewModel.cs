using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_Floor_Project.Models;
using Master_Floor_Project.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Master_Floor_Project.ViewModels
{
    public partial class PartnerEditViewModel : ViewModelBase
    {
        // Сервис для работы с партнерами
        private readonly IPartnerService _partnerService;
        public event Action? OnRequestClose; // Событие запроса закрытия окна
        private int? _editingPartnerId; // ID редактируемого партнера (null для нового)

        // Свойства формы с валидацией данных
        [ObservableProperty]
        [Required(ErrorMessage = "Наименование компании обязательно")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _name = ""; // Наименование компании (обязательное поле)

        [ObservableProperty]
        [RegularExpression(@"^(\d{10}|\d{12})$", ErrorMessage = "ИНН должен состоять из 10 или 12 цифр")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string? _inn; // ИНН партнера с проверкой формата

        [ObservableProperty]
        [EmailAddress(ErrorMessage = "Некорректный формат Email адреса")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string? _email; // Email с валидацией формата

        [ObservableProperty] private string? _type; // Тип организации (ИП, ООО и т.д.)
        [ObservableProperty] private string? _address; // Юридический адрес
        [ObservableProperty] private string? _directorName; // ФИО директора
        [ObservableProperty] private string? _phone; // Контактный телефон
        [ObservableProperty] private string? _rating; // Рейтинг в виде звездочек (⭐)

        public PartnerEditViewModel()
        {
            _partnerService = new PartnerService();
            ValidateAllProperties(); // Первоначальная валидация полей
        }

        // Загрузка данных партнера для редактирования
        public void LoadPartner(Partner partner)
        {
            _editingPartnerId = partner.PartnerId; // Сохранение ID для обновления
            Name = partner.Name;
            Type = partner.Type;
            Address = partner.Address;
            Inn = partner.Inn;
            DirectorName = partner.DirectorName;
            Phone = partner.Phone;
            Email = partner.Email;

            // Преобразование числового рейтинга в строку со звездочками
            Rating = partner.Rating.HasValue ? new string('⭐', partner.Rating.Value) : null;
        }

        // Проверка возможности сохранения (отсутствие ошибок валидации)
        private bool CanSave() => !HasErrors;

        // Команда сохранения партнера с проверкой валидации
        [RelayCommand(CanExecute = nameof(CanSave))]
        private async Task SaveAsync()
        {
            ValidateAllProperties(); // Повторная валидация перед сохранением
            if (HasErrors) return; // Прерывание если есть ошибки

            try
            {
                // Преобразование строки рейтинга обратно в число (количество звездочек)
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

                // Определение операции: обновление или создание
                if (_editingPartnerId.HasValue)
                {
                    // Установка ID для обновления
                    partner.PartnerId = _editingPartnerId.Value;

                    // Обновление существующего
                    await _partnerService.UpdatePartnerAsync(partner);
                }
                else
                {
                    // Создание нового
                    await _partnerService.AddPartnerAsync(partner);
                }

                // Уведомление об изменении партнеров
                EventAggregator.PublishPartnersChanged();

                // Закрытие окна после успешного сохранения
                OnRequestClose?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при сохранении партнера: {ex}");
            }
        }
    }
}