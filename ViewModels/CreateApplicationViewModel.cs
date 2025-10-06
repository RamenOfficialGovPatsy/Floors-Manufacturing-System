using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Master_Floor_Project.Models;

namespace Master_Floor_Project.ViewModels
{
    public partial class CreateApplicationViewModel : ViewModelBase
    {
        // Свойство для хранения итоговой суммы
        [ObservableProperty]
        private decimal _totalSum;

        // Коллекция партнеров для ComboBox
        public ObservableCollection<Partner> AllPartners { get; set; } = new();

        // Коллекция товаров, добавленных в заявку
        public ObservableCollection<ApplicationItem> Items { get; set; } = new();

        public CreateApplicationViewModel()
        {
            LoadInitialData();
        }

        private void LoadInitialData()
        {
            // Загружаем тестовых партнеров для выбора
            AllPartners.Add(new Partner { Name = "ООО \"Вектор\"" });
            AllPartners.Add(new Partner { Name = "ООО \"Стройка\"" });
            AllPartners.Add(new Partner { Name = "ИП Сидоров А.В." });

            // Добавляем тестовые товары в список для примера
            Items.Add(new ApplicationItem { ProductName = "Ламинат", Quantity = 5, Price = 1500 });
            Items.Add(new ApplicationItem { ProductName = "Паркетная доска", Quantity = 2, Price = 2800 });
            Items.Add(new ApplicationItem { ProductName = "Плинтус МДФ", Quantity = 10, Price = 450 });

            // Рассчитываем итоговую сумму
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            TotalSum = 0;
            foreach (var item in Items)
            {
                TotalSum += item.Sum;
            }
        }

        // Команды для кнопок
        [RelayCommand]
        private void AddProduct()
        {
            // TODO: Реализовать логику открытия окна/диалога для добавления нового товара
        }

        [RelayCommand]
        private void SaveApplication()
        {
            // TODO: Реализовать логику сохранения заявки в БД
        }

        [RelayCommand]
        private void Cancel()
        {
            // TODO: Реализовать логику закрытия окна (позже)
        }
    }
}