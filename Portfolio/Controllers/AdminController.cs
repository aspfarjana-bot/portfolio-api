using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Portfolio.Filters;
using Portfolio.Models;
using Portfolio.Models.DTOs;

namespace Portfolio.Controllers
{
    [JwtAuthentication]
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
        private PortfolioDbContext db = new PortfolioDbContext();

        // GET: api/admin/dashboard
        [HttpGet]
        [Route("dashboard")]
        public async Task<IHttpActionResult> GetDashboardStats()
        {
            var stats = new
            {
                ProjectCount = await db.Projects.CountAsync(),
                SkillCount = await db.Skills.CountAsync(),
                TestimonialCount = await db.Testimonials.CountAsync(),
                MessageCount = await db.ContactMessages.CountAsync()
            };
            return Ok(stats);
        }

        // GET: api/admin/test
        [HttpGet]
        [Route("test")]
        public IHttpActionResult TestAuth()
        {
            return Ok(new { Message = "Authenticated successfully!", User = User.Identity.Name });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
