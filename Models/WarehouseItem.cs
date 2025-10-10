using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Master_Floor_Project.Models
{
    [Table("warehouse")]
    public class WarehouseItem
    {
        [Key]
        [Column("warehouse_id")]
        public int WarehouseId { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;

        [Column("quantity_on_hand")]
        public int QuantityOnHand { get; set; }
    }
}