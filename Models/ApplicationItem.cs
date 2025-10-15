using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Master_Floor_Project.Models
{
    [Table("application_items")]
    public class ApplicationItem
    {
        [Key]
        [Column("item_id")]
        public int ApplicationItemId { get; set; } // Уникальный идентификатор позиции

        [Column("application_id")]
        public int ApplicationId { get; set; } // ID заявки, к которой относится позиция

        [ForeignKey("ApplicationId")]
        public Application Application { get; set; } = null!; // Навигационное свойство к заявке

        [Column("product_id")]
        public int ProductId { get; set; } // ID продукта в позиции

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!; // Навигационное свойство к продукту

        [Column("quantity")]
        public int Quantity { get; set; } // Количество продукта в позиции

        [NotMapped]
        public string ProductName { get; set; } = string.Empty; // Название продукта для отображения

        // Общая стоимость позиции (количество × цена)
        [NotMapped]
        public decimal Sum => Quantity * Price;

        // Цена продукта для расчетов
        [NotMapped]
        public decimal Price { get; set; }
    }
}