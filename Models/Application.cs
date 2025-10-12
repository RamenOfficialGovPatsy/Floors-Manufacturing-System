using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Master_Floor_Project.Models
{
    [Table("applications")]
    public class Application
    {
        [Key]
        [Column("application_id")]
        public int ApplicationId { get; set; }

        [Column("partner_id")]
        public int PartnerId { get; set; }

        [ForeignKey("PartnerId")]
        public virtual Partner Partner { get; set; } = null!;

        [Column("manager_id")]
        public int? ManagerId { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { get; set; }

        [Column("status")]
        public string Status { get; set; } = string.Empty;

        // Навигационное свойство для позиций заявки
        public virtual List<ApplicationItem> ApplicationItems { get; set; } = new();

        [NotMapped]
        public string ApplicationNumber => $"Z-{DateCreated:yyyy}-{ApplicationId:D3}";

        [NotMapped]
        public string PartnerName => Partner?.Name ?? "Неизвестный партнер"; // ✅ Убрали хардкод
    }
}