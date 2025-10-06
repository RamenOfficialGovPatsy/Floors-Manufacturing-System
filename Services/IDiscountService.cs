namespace Master_Floor_Project.Services
{
    public interface IDiscountService
    {
        // Метод, который принимает сумму и возвращает процент скидки
        decimal CalculateDiscount(decimal totalAmount);
    }
}