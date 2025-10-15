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
        public int HistoryId { get; set; } // Уникальный идентификатор записи истории

        [Column("partner_id")]
        public int PartnerId { get; set; } // ID партнера, совершившего продажу

        [ForeignKey("PartnerId")]
        public virtual Partner Partner { get; set; } = null!; // Навигационное свойство к партнеру

        [Column("product_id")]
        public int ProductId { get; set; } // ID проданного продукта

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; } = null!; // Навигационное свойство к продукту

        [Column("quantity")]
        public int Quantity { get; set; } // Количество проданного товара

        [Column("sale_date")]
        public DateTime SaleDate { get; set; } // Дата и время продажи
    }
}