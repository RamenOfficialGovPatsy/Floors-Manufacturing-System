using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input; // <-- ВОТ ОНА, недостающая строка!
using Master_Floor_Project.Data;
using Master_Floor_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Master_Floor_Project.ViewModels
{
    public partial class PartnersViewModel : ViewModelBase
    {
        private readonly AppDbContext _context;

        [ObservableProperty]
        private ObservableCollection<Partner> _partners;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EditPartnerCommand))]
        [NotifyCanExecuteChangedFor(nameof(DeletePartnerCommand))]
        private Partner? _selectedPartner;

        public PartnersViewModel()
        {
            _context = new AppDbContext();
            Partners = new ObservableCollection<Partner>();
        }

        public async Task LoadPartnersAsync()
        {
            try
            {
                Partners.Clear();
                var partnersList = await _context.Partners.ToListAsync();
                foreach (var partner in partnersList)
                {
                    Partners.Add(partner);
                }
            }
            catch (Exception ex)
            {
                // В будущем здесь будет логирование или окно с ошибкой
                Console.WriteLine($"Ошибка при загрузке партнеров: {ex.Message}");
            }
        }

        [RelayCommand]
        private void AddPartner()
        {
            // TODO: Реализовать логику открытия окна добавления партнера
        }

        [RelayCommand(CanExecute = nameof(CanEditOrDeletePartner))]
        private void EditPartner(Partner? partner)
        {
            // TODO: Реализовать логику открытия окна редактирования
        }

        [RelayCommand(CanExecute = nameof(CanEditOrDeletePartner))]
        private void DeletePartner(Partner? partner)
        {
            // TODO: Реализовать логику удаления партнера
        }

        private bool CanEditOrDeletePartner()
        {
            return SelectedPartner != null;
        }
    }
}