using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Master_Floor_Project.Models
{
    [Table("application_items")]
    public class ApplicationItem
    {
        [Key]
        [Column("item_id")]
        public int ApplicationItemId { get; set; }

        [Column("application_id")]
        public int ApplicationId { get; set; }

        [ForeignKey("ApplicationId")]
        public Application Application { get; set; } = null!;

        [Column("product_id")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;

        [Column("quantity")]
        public int Quantity { get; set; }

        // Добавляем свойство для имени продукта, которое можно устанавливать
        [NotMapped]
        public string ProductName { get; set; } = string.Empty;

        [NotMapped]
        public decimal Sum => Quantity * Price;

        // Добавляем свойство Price для расчетов
        [NotMapped]
        public decimal Price { get; set; }
    }
}