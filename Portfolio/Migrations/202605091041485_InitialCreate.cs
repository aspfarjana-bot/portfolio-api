namespace Portfolio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        PasswordHash = c.String(nullable: false),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Subject = c.String(),
                        Message = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Title = c.String(),
                        Bio = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Location = c.String(),
                        ImageUrl = c.String(),
                        GithubUrl = c.String(),
                        LinkedinUrl = c.String(),
                        TwitterUrl = c.String(),
                        ResumeUrl = c.String(),
                        RotatingTitles = c.String(),
                        DiscordUrl = c.String(),
                        InstagramUrl = c.String(),
                        LogoText = c.String(),
                        LogoImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Category = c.String(),
                        Description = c.String(),
                        ImageUrl = c.String(),
                        Technologies = c.String(),
                        GithubLink = c.String(),
                        LiveLink = c.String(),
                        DisplayOrder = c.Int(nullable: false),
                        ProfileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.ProfileId, cascadeDelete: true)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Category = c.String(),
                        Proficiency = c.String(),
                        IconName = c.String(),
                        DisplayOrder = c.Int(nullable: false),
                        ProfileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.ProfileId, cascadeDelete: true)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.Testimonials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientName = c.String(nullable: false),
                        ClientRole = c.String(),
                        Company = c.String(),
                        Feedback = c.String(nullable: false),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skills", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.Projects", "ProfileId", "dbo.Profiles");
            DropIndex("dbo.Skills", new[] { "ProfileId" });
            DropIndex("dbo.Projects", new[] { "ProfileId" });
            DropTable("dbo.Testimonials");
            DropTable("dbo.Skills");
            DropTable("dbo.Projects");
            DropTable("dbo.Profiles");
            DropTable("dbo.ContactMessages");
            DropTable("dbo.AdminUsers");
        }
    }
}
