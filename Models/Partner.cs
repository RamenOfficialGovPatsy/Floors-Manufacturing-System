using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Master_Floor_Project.Models
{
    [Table("partners")]
    public class Partner
    {
        [Key]
        [Column("partner_id")]
        public int PartnerId { get; set; } // Уникальный идентификатор партнера

        [Column("type")]
        public string? Type { get; set; } // Тип партнера (ИП, ООО, ЗАО и т.д.)

        [Column("name")]
        public string Name { get; set; } = string.Empty; // Наименование компании партнера

        [Column("address")]
        public string? Address { get; set; } // Юридический адрес партнера

        [Column("inn")]
        public string Inn { get; set; } = string.Empty; // ИНН партнера

        [Column("director_name")]
        public string? DirectorName { get; set; } // ФИО директора компании

        [Column("phone")]
        public string? Phone { get; set; } // Контактный телефон

        [Column("email")]
        public string? Email { get; set; } // Email для связи

        [Column("logo_path")]
        public string? LogoPath { get; set; } // Путь к файлу логотипа

        [Column("rating")]
        public int? Rating { get; set; } // Рейтинг партнера (от 1 до 5)

        // Список заявок партнера
        public virtual List<Application> Applications { get; set; } = new();
    }
}