using System;

namespace Master_Floor_Project.Models
{
    public class Partner
    {
        public int PartnerId { get; set; }
        public string? Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Inn { get; set; }
        public string? DirectorName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? LogoPath { get; set; }
        public int? Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}