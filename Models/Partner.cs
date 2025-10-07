using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Master_Floor_Project.Models
{
    [Table("partners")] // Явно указываем имя таблицы
    public class Partner
    {
        [Key] // Указываем, что это первичный ключ
        [Column("partner_id")]
        public int PartnerId { get; set; }

        [Column("type")]
        public string? Type { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("address")]
        public string? Address { get; set; }

        [Column("inn")]
        public string Inn { get; set; } = string.Empty;

        [Column("director_name")]
        public string? DirectorName { get; set; }

        [Column("phone")]
        public string? Phone { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("logo_path")]
        public string? LogoPath { get; set; }

        [Column("rating")]
        public int? Rating { get; set; }
    }
}