namespace Master_Floor_Project.Models
{
    public class ApplicationItem
    {
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Sum => Quantity * Price; // Свойство, которое само считает сумму
    }
}