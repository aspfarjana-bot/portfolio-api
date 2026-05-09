using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Portfolio.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Technologies { get; set; } // Comma separated or JSON
        public string GithubLink { get; set; }
        public string LiveLink { get; set; }
        public int DisplayOrder { get; set; }

        public int ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
