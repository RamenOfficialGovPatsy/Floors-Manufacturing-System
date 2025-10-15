namespace Master_Floor_Project.Services
{
    public class DiscountService : IDiscountService
    {
        // Расчет процента скидки по прогрессивной шкале
        public decimal CalculateDiscount(decimal totalAmount)
        {
            // Используем switch expression для краткости и читаемости
            return totalAmount switch
            {
                > 300000 => 0.15m, // 15% скидка для заказов свыше 300 000 руб
                > 50000 => 0.10m, // 10% скидка для заказов свыше 50 000 руб
                > 10000 => 0.05m, // 5% скидка для заказов свыше 10 000 руб
                _ => 0m     // Без скидки для заказов до 10 000 руб
            };
        }
    }
}