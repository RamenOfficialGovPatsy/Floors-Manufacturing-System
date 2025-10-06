using System;

namespace Master_Floor_Project.Models
{
    public class Application
    {
        public string ApplicationNumber { get; set; } = string.Empty;
        public string PartnerName { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}