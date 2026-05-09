using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Portfolio.Models
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Title { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        public string GithubUrl { get; set; }
        public string LinkedinUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string ResumeUrl { get; set; }
        public string RotatingTitles { get; set; }
        public string DiscordUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string LogoText { get; set; }
        public string LogoImageUrl { get; set; }

        // Relationships
        public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
