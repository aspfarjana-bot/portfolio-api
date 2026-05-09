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
    [RoutePrefix("api/projects")]
    public class ProjectsController : ApiController
    {
        private PortfolioDbContext db = new PortfolioDbContext();

        // GET: api/Projects
        [Route("")]
        public async Task<IHttpActionResult> GetProjects()
        {
            var projects = await db.Projects.OrderBy(p => p.DisplayOrder).ToListAsync();
            var dtos = projects.Select(p => new ProjectDTO
            {
                Id = p.Id,
                Title = p.Title,
                Category = p.Category,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Technologies = p.Technologies,
                GithubLink = p.GithubLink,
                LiveLink = p.LiveLink,
                DisplayOrder = p.DisplayOrder,
                ProfileId = p.ProfileId
            }).ToList();
            return Ok(dtos);
        }

        // GET: api/Projects/5
        [Route("{id}")]
        public async Task<IHttpActionResult> GetProject(int id)
        {
            var project = await db.Projects.FindAsync(id);
            if (project == null) return NotFound();

            var dto = new ProjectDTO
            {
                Id = project.Id,
                Title = project.Title,
                Category = project.Category,
                Description = project.Description,
                ImageUrl = project.ImageUrl,
                Technologies = project.Technologies,
                GithubLink = project.GithubLink,
                LiveLink = project.LiveLink,
                DisplayOrder = project.DisplayOrder,
                ProfileId = project.ProfileId
            };
            return Ok(dto);
        }

        // POST: api/Projects
        [HttpPost]
        [JwtAuthentication]
        [Route("")]
        public async Task<IHttpActionResult> PostProject(Project project)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            db.Projects.Add(project);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = project.Id }, project);
        }

        // PUT: api/Projects/5
        [HttpPut, HttpPost]
        [Route("{id}")]
        public async Task<IHttpActionResult> PutProject(int id, Project project)
        {
            try 
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                
                var existing = await db.Projects.FindAsync(id);
                if (existing == null) return NotFound();

                existing.Title = project.Title;
                existing.Category = project.Category;
                existing.Description = project.Description;
                existing.ImageUrl = project.ImageUrl;
                existing.Technologies = project.Technologies;
                existing.GithubLink = project.GithubLink;
                existing.LiveLink = project.LiveLink;
                existing.DisplayOrder = project.DisplayOrder;
                
                // Fallback
                existing.ProfileId = project.ProfileId != 0 ? project.ProfileId : 1;

                await db.SaveChangesAsync();

                return Ok(new { Message = "Successfully updated via EF" });
            }
            catch (Exception ex)
            {
                var fullMessage = ex.Message + (ex.InnerException != null ? " INNER: " + ex.InnerException.Message : "");
                return BadRequest("CRITICAL ERROR: " + fullMessage + " STACK: " + ex.StackTrace);
            }
        }

        // DELETE: api/Projects/5
        [HttpDelete]
        [JwtAuthentication]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteProject(int id)
        {
            Project project = await db.Projects.FindAsync(id);
            if (project == null) return NotFound();

            db.Projects.Remove(project);
            await db.SaveChangesAsync();

            return Ok(project);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
