using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Portfolio.Models
{
    public class ContactMessage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
