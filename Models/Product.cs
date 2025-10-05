using System.ComponentModel.DataAnnotations.Schema;

namespace Master_Floor_Project.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Article { get; set; } = string.Empty;
        public string? Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImagePath { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? MinPricePartner { get; set; }
    }
}