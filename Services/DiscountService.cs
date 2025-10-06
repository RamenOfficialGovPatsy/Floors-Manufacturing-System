namespace Master_Floor_Project.Services
{
    public class DiscountService : IDiscountService
    {
        public decimal CalculateDiscount(decimal totalAmount)
        {
            // Используем switch expression для краткости и читаемости
            return totalAmount switch
            {
                > 300000 => 0.15m, // 15%
                > 50000 => 0.10m, // 10%
                > 10000 => 0.05m, // 5%
                _ => 0m     // 0%
            };
        }
    }
}