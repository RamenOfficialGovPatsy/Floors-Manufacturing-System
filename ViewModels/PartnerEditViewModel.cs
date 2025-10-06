using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_Floor_Project.Models;

namespace Master_Floor_Project.ViewModels
{
    public partial class PartnerEditViewModel : ViewModelBase
    {
        [ObservableProperty]
        [Required(ErrorMessage = "Наименование компании обязательно")]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string? _name;

        [ObservableProperty]
        private string? _address;

        [ObservableProperty]
        private string? _inn;

        [ObservableProperty]
        private string? _directorName;

        [ObservableProperty]
        private string? _phone;

        [ObservableProperty]
        private string? _email;

        [ObservableProperty]
        private string? _rating;

        public PartnerEditViewModel()
        {
            ValidateAllProperties();
        }

        // ИЗМЕНЕНИЕ: Упрощаем логику для надежности.
        // Теперь кнопка будет активна, если поле Name не пустое.
        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }

        [RelayCommand(CanExecute = nameof(CanSave))]
        private void Save()
        {
            // Логика сохранения данных в БД
        }
    }
}