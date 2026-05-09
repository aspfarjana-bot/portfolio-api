using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portfolio.Models.DTOs
{
    public class ProfileDTO
    {
        public int Id { get; set; }
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
    }

    public class SkillDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Proficiency { get; set; }
        public string IconName { get; set; }
        public int DisplayOrder { get; set; }
        public int ProfileId { get; set; }
    }

    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Technologies { get; set; }
        public string GithubLink { get; set; }
        public string LiveLink { get; set; }
        public int DisplayOrder { get; set; }
        public int ProfileId { get; set; }
    }

    public class TestimonialDTO
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ClientRole { get; set; }
        public string Company { get; set; }
        public string Feedback { get; set; }
        public string ImageUrl { get; set; }
    }

    public class ContactMessageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
