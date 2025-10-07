using System;
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

        [Column("manager_id")]
        public int? ManagerId { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { get; set; }

        [Column("status")]
        public string Status { get; set; } = string.Empty;
    }
}