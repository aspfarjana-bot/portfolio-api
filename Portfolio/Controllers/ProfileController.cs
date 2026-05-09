using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Portfolio.Models;
using Portfolio.Models.DTOs;
using Portfolio.Filters;

namespace Portfolio.Controllers
{
    public class ProfileController : ApiController
    {
        private PortfolioDbContext db = new PortfolioDbContext();

        // GET: api/Profile
        public async Task<IHttpActionResult> GetProfile()
        {
            var profile = await db.Profile.FirstOrDefaultAsync();
            if (profile == null) return NotFound();

            var dto = new ProfileDTO
            {
                Id = profile.Id,
                Name = profile.Name,
                Title = profile.Title,
                Bio = profile.Bio,
                Email = profile.Email,
                Phone = profile.Phone,
                Location = profile.Location,
                ImageUrl = profile.ImageUrl,
                GithubUrl = profile.GithubUrl,
                LinkedinUrl = profile.LinkedinUrl,
                TwitterUrl = profile.TwitterUrl,
                ResumeUrl = profile.ResumeUrl,
                RotatingTitles = profile.RotatingTitles,
                DiscordUrl = profile.DiscordUrl,
                InstagramUrl = profile.InstagramUrl,
                LogoText = profile.LogoText,
                LogoImageUrl = profile.LogoImageUrl
            };

            return Ok(dto);
        }

        // PUT: api/Profile/1
        [HttpPut]
        [JwtAuthentication]
        public async Task<IHttpActionResult> PutProfile(int id, Profile profile)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != profile.Id) return BadRequest();

            db.Entry(profile).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (!db.Profile.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
