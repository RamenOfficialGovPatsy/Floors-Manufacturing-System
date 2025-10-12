using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Master_Floor_Project.Models
{
    [Table("sales_history")]
    public class SalesHistory
    {
        [Key]
        [Column("history_id")]
        public int HistoryId { get; set; }

        [Column("partner_id")]
        public int PartnerId { get; set; }

        [ForeignKey("PartnerId")]
        public virtual Partner Partner { get; set; } = null!;

        [Column("product_id")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; } = null!;

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("sale_date")]
        public DateTime SaleDate { get; set; }
    }
}