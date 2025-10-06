using System.Collections.ObjectModel;
using System.Linq; // <-- Добавьте
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_Floor_Project.Models;
using Master_Floor_Project.Services; // <-- Добавьте

namespace Master_Floor_Project.ViewModels
{
    public partial class CreateApplicationViewModel : ViewModelBase
    {
        private readonly IDiscountService _discountService;

        [ObservableProperty]
        private decimal _subTotal; // Переименовали TotalSum в SubTotal

        [ObservableProperty]
        private decimal _discountPercentage;

        [ObservableProperty]
        private decimal _finalTotal;

        public ObservableCollection<Partner> AllPartners { get; set; } = new();
        public ObservableCollection<ApplicationItem> Items { get; set; } = new();

        public CreateApplicationViewModel()
        {
            _discountService = new DiscountService();
            LoadInitialData();
        }

        private void LoadInitialData()
        {
            AllPartners.Add(new Partner { Name = "ООО \"Вектор\"" });
            AllPartners.Add(new Partner { Name = "ООО \"Стройка\"" });
            AllPartners.Add(new Partner { Name = "ИП Сидоров А.В." });

            Items.Add(new ApplicationItem { ProductName = "Ламинат", Quantity = 5, Price = 1500 });
            Items.Add(new ApplicationItem { ProductName = "Паркетная доска", Quantity = 2, Price = 2800 });
            Items.Add(new ApplicationItem { ProductName = "Плинтус МДФ", Quantity = 10, Price = 450 });

            CalculateTotals();
        }

        private void CalculateTotals()
        {
            SubTotal = Items.Sum(item => item.Sum);
            DiscountPercentage = _discountService.CalculateDiscount(SubTotal);
            FinalTotal = SubTotal * (1 - DiscountPercentage);
        }

        [RelayCommand]
        private void AddProduct() { /* ... */ }

        [RelayCommand]
        private void SaveApplication() { /* ... */ }

        [RelayCommand]
        private void Cancel() { /* ... */ }
    }
}