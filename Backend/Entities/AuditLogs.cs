using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class AuditLogs
    {
        [Key]
        public int LogId { get; set; }

        public int UserId { get; set; } // Foreign key to Users table

        [Required]
        public string Action { get; set; }

        public string IpAddress { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}