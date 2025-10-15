using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Master_Floor_Project.Models
{
    [Table("products")]
    public class Product
    {
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; } // Уникальный идентификатор продукта

        [Column("article")]
        public string Article { get; set; } = string.Empty; // Артикул продукта

        [Column("type")]
        public string? Type { get; set; } // Тип продукции (паркет, ламинат и т.д.)

        [Column("name")]
        public string Name { get; set; } = string.Empty; // Наименование продукта

        [Column("description")]
        public string? Description { get; set; } // Описание продукта

        [Column("image_path")]
        public string? ImagePath { get; set; } // Путь к изображению продукта

        [Column("min_price_partner", TypeName = "decimal(10, 2)")]
        public decimal? MinPricePartner { get; set; } // Минимальная цена для партнеров
    }
}