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
    [RoutePrefix("api/skills")]
    public class SkillsController : ApiController
    {
        private PortfolioDbContext db = new PortfolioDbContext();

        // GET: api/Skills
        [Route("")]
        public async Task<IHttpActionResult> GetSkills()
        {
            var skills = await db.Skills.OrderBy(s => s.DisplayOrder).ToListAsync();
            var dtos = skills.Select(s => new SkillDTO
            {
                Id = s.Id,
                Name = s.Name,
                Category = s.Category,
                Proficiency = s.Proficiency,
                IconName = s.IconName,
                DisplayOrder = s.DisplayOrder,
                ProfileId = s.ProfileId
            }).ToList();
            return Ok(dtos);
        }

        // POST: api/Skills
        [HttpPost]
        [JwtAuthentication]
        [Route("")]
        public async Task<IHttpActionResult> PostSkill(Skill skill)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            db.Skills.Add(skill);
            await db.SaveChangesAsync();

            return Ok(skill);
        }

        // PUT: api/Skills/5
        [HttpPut, HttpPost]
        [Route("{id}")]
        public async Task<IHttpActionResult> PutSkill(int id, Skill skill)
        {
            try 
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                
                var existing = await db.Skills.FindAsync(id);
                if (existing == null) return NotFound();

                // Map properties carefully
                existing.Name = skill.Name;
                existing.Category = skill.Category;
                existing.Proficiency = skill.Proficiency;
                existing.IconName = skill.IconName;
                existing.DisplayOrder = skill.DisplayOrder;
                
                // Fallback ProfileId
                existing.ProfileId = skill.ProfileId != 0 ? skill.ProfileId : 1;

                await db.SaveChangesAsync();

                return Ok(new { Message = "Successfully updated via EF" });
            }
            catch (Exception ex)
            {
                // Return full details including inner exception if exists
                var fullMessage = ex.Message + (ex.InnerException != null ? " INNER: " + ex.InnerException.Message : "");
                return BadRequest("CRITICAL ERROR: " + fullMessage + " STACK: " + ex.StackTrace);
            }
        }

        // DELETE: api/Skills/5
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteSkill(int id)
        {
            Skill skill = await db.Skills.FindAsync(id);
            if (skill == null) return NotFound();

            db.Skills.Remove(skill);
            await db.SaveChangesAsync();

            return Ok(skill);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
