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
        public int ApplicationId { get; set; } // Уникальный идентификатор заявки

        [Column("partner_id")]
        public int PartnerId { get; set; } // ID партнера, создавшего заявку

        [ForeignKey("PartnerId")]
        public virtual Partner Partner { get; set; } = null!; // Навигационное свойство к партнеру

        [Column("manager_id")]
        public int? ManagerId { get; set; } // ID менеджера, обрабатывающего заявку (может быть null)

        [Column("date_created")]
        public DateTime DateCreated { get; set; } // Дата и время создания заявки

        [Column("status")]
        public string Status { get; set; } = string.Empty; // Текущий статус заявки

        // Навигационное свойство для позиций заявки
        public virtual List<ApplicationItem> ApplicationItems { get; set; } = new();

        // Форматированный номер заявки
        [NotMapped]
        public string ApplicationNumber => $"Z-{DateCreated:yyyy}-{ApplicationId:D3}";

        // Название партнера для отображения
        [NotMapped]
        public string PartnerName => Partner?.Name ?? "Неизвестный партнер"; // ✅ Убрали хардкод
    }
}