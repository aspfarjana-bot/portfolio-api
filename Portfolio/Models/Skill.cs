using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Portfolio.Models
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Category { get; set; } // e.g., Frontend, Backend, Tool
        public string Proficiency { get; set; } // e.g., Beginner, Intermediate, Advanced
        public string IconName { get; set; } // Lucide icon name
        public int DisplayOrder { get; set; }

        public int ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
