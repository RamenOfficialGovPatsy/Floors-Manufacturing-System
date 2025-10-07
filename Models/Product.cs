using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Master_Floor_Project.Models
{
    [Table("products")]
    public class Product
    {
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("article")]
        public string Article { get; set; } = string.Empty;

        [Column("type")]
        public string? Type { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Column("image_path")]
        public string? ImagePath { get; set; }

        [Column("min_price_partner", TypeName = "decimal(10, 2)")]
        public decimal? MinPricePartner { get; set; }
    }
}