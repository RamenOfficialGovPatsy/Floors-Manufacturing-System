using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_Floor_Project.Models;
using Master_Floor_Project.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Master_Floor_Project.Windows;

namespace Master_Floor_Project.ViewModels
{
    public partial class PartnersViewModel : ViewModelBase
    {
        // Сервис работы с партнерами
        private readonly IPartnerService _partnerService;

        // Коллекция партнеров для отображения
        [ObservableProperty] private ObservableCollection<Partner> _partners;

        // Выбранный партнер в списке
        [ObservableProperty][NotifyCanExecuteChangedFor(nameof(EditPartnerCommand))][NotifyCanExecuteChangedFor(nameof(DeletePartnerCommand))] private Partner? _selectedPartner;
        [ObservableProperty] private bool _isLoading = true; // Флаг загрузки данных

        public PartnersViewModel()
        {
            _partnerService = new PartnerService();
            Partners = new ObservableCollection<Partner>();

            // Подписка на событие изменения партнеров для автоматического обновления списка
            EventAggregator.PartnersChanged += OnPartnersChanged;
        }

        // Обработчик события изменения данных партнеров
        private async void OnPartnersChanged()
        {
            await LoadPartnersAsync(); // Перезагрузка списка при изменениях
        }

        // Загрузка списка партнеров из базы данных
        public async Task LoadPartnersAsync()
        {
            IsLoading = true; // Включение индикатора загрузки
            try
            {
                Partners.Clear(); // Очистка текущего списка

                // Получение данных
                var partnersList = await _partnerService.GetPartnersAsync();
                foreach (var partner in partnersList)
                {
                    Partners.Add(partner); // Добавление партнеров в коллекцию
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при загрузке партнеров: {ex.Message}");
            }
            finally
            {
                IsLoading = false; // Выключение индикатора загрузки
            }
        }

        // Команда открытия формы добавления нового партнера
        [RelayCommand]
        private void AddPartner()
        {
            // Создание VM для новой формы
            var editViewModel = new PartnerEditViewModel();

            // Создание окна
            var editWindow = new PartnerEditWindow { DataContext = editViewModel };
            editWindow.Show(); // Отображение окна
        }

        // Команда редактирования выбранного партнера
        [RelayCommand(CanExecute = nameof(CanEditOrDeletePartner))]
        private void EditPartner(Partner? partner)
        {
            if (partner is null) return; // Проверка что партнер выбран
            var editViewModel = new PartnerEditViewModel(); // Создание VM для редактирования
            editViewModel.LoadPartner(partner); // Загрузка данных партнера
            var editWindow = new PartnerEditWindow { DataContext = editViewModel }; // Создание окна
            editWindow.Show(); // Отображение окна редактирования
        }

        // Команда удаления выбранного партнера
        [RelayCommand(CanExecute = nameof(CanEditOrDeletePartner))]
        private async Task DeletePartnerAsync(Partner? partner)
        {
            if (partner is null) return; // Проверка что партнер выбран
            try
            {
                // Удаление из БД
                await _partnerService.DeletePartnerAsync(partner.PartnerId);
                await LoadPartnersAsync(); // Обновление списка после удаления
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при удалении партнера: {ex.Message}");
                Debug.WriteLine($"Inner exception: {ex.InnerException?.Message}");
            }
        }

        // Проверка возможности редактирования/удаления (партнер должен быть выбран)
        private bool CanEditOrDeletePartner() => SelectedPartner != null;
    }
}