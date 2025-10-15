using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Master_Floor_Project.Models
{
    [Table("managers")]
    public class Manager
    {
        [Key]
        [Column("manager_id")]
        public int ManagerId { get; set; } // Уникальный идентификатор менеджера

        [Column("full_name")]
        public string FullName { get; set; } = string.Empty; // Полное имя менеджера

        [Column("email")]
        public string Email { get; set; } = string.Empty; // Email менеджера для связи
    }
}