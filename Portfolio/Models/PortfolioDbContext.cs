using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace Portfolio.Models
{
    public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext() : base("PortfolioConnection")
        {
            Database.SetInitializer(new PortfolioDbInitializer());
        }

        public DbSet<Profile> Profile { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Optional: Fluent API configurations
            modelBuilder.Entity<Skill>()
                .HasRequired<Profile>(s => s.Profile)
                .WithMany(p => p.Skills)
                .HasForeignKey<int>(s => s.ProfileId);

            modelBuilder.Entity<Project>()
                .HasRequired<Profile>(pr => pr.Profile)
                .WithMany(p => p.Projects)
                .HasForeignKey<int>(pr => pr.ProfileId);

            base.OnModelCreating(modelBuilder);
        }
    }

    public class PortfolioDbInitializer : CreateDatabaseIfNotExists<PortfolioDbContext>
    {
        protected override void Seed(PortfolioDbContext context)
        {
            var profile = new Profile
            {
                Name = "Farjana Akter",
                Title = "Full Stack Web Developer",
                Bio = "Highly motivated and results-oriented Software Engineer with expertise in building scalable web applications using modern technologies like React and .NET.",
                Email = "aspfarjana@gmail.com",
                Phone = "017422-7717",
                Location = "Dhaka, Bangladesh",
                GithubUrl = "https://github.com/farjana",
                LinkedinUrl = "https://linkedin.com/in/farjana",
                TwitterUrl = "https://twitter.com/farjana",
                ResumeUrl = "#",
                RotatingTitles = "Full Stack Developer,Programmer,HTML,CSS,JavaScript,Angular,React,C#,Web API,ASP.NET Core,Entity Framework",
                LogoText = "FA"
            };

            context.Profile.Add(profile);
            context.SaveChanges(); // Persist profile to get ID

            var projects = new List<Project>
            {
                new Project { ProfileId = profile.Id, Title = "E-commerce Platform", Category = "E-commerce", ImageUrl = "https://images.unsplash.com/photo-1472851294608-062f824d29cc?auto=format&fit=crop&q=80&w=800", Description = "A full-stack e-commerce solution with React and .NET Core.", GithubLink = "#", LiveLink = "#", DisplayOrder = 1 },
                new Project { ProfileId = profile.Id, Title = "Portfolio Website", Category = "Web Design", ImageUrl = "https://images.unsplash.com/photo-1545235617-9465d2a55698?auto=format&fit=crop&q=80&w=800", Description = "A modern, minimal portfolio for developers.", GithubLink = "#", LiveLink = "#", DisplayOrder = 2 },
                new Project { ProfileId = profile.Id, Title = "IELTS Learning App", Category = "Education", ImageUrl = "https://images.unsplash.com/photo-1522202176988-66273c2fd55f?auto=format&fit=crop&q=80&w=800", Description = "Online education platform with real-time test simulations.", GithubLink = "#", LiveLink = "#", DisplayOrder = 3 }
            };
            projects.ForEach(p => context.Projects.Add(p));

            var skills = new List<Skill>
            {
                new Skill { ProfileId = profile.Id, Name = "React.js", Category = "Frontend", Proficiency = "Advanced", IconName = "Layout", DisplayOrder = 1 },
                new Skill { ProfileId = profile.Id, Name = "ASP.NET Core", Category = "Backend", Proficiency = "Intermediate", IconName = "Terminal", DisplayOrder = 2 },
                new Skill { ProfileId = profile.Id, Name = "SQL Server", Category = "Backend", Proficiency = "Intermediate", IconName = "Database", DisplayOrder = 3 }
            };
            skills.ForEach(s => context.Skills.Add(s));

            context.Testimonials.Add(new Testimonial { ClientName = "Farjana Akter", ClientRole = "CEO", Company = "TechCorp", Feedback = "Farjana is an exceptional developer.", ImageUrl = "https://i.pravatar.cc/150?u=1" });

            // Seed Admin User
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes("admin123");
                var hash = sha256.ComputeHash(bytes);
                context.AdminUsers.Add(new AdminUser 
                { 
                    Username = "admin", 
                    PasswordHash = Convert.ToBase64String(hash)
                });
            }

            base.Seed(context);
        }
    }
}
