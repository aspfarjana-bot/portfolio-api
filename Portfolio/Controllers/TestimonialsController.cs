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
    public class TestimonialsController : ApiController
    {
        private PortfolioDbContext db = new PortfolioDbContext();

        // GET: api/Testimonials
        public async Task<IHttpActionResult> GetTestimonials()
        {
            var testimonials = await db.Testimonials.ToListAsync();
            var dtos = testimonials.Select(t => new TestimonialDTO
            {
                Id = t.Id,
                ClientName = t.ClientName,
                ClientRole = t.ClientRole,
                Company = t.Company,
                Feedback = t.Feedback,
                ImageUrl = t.ImageUrl
            }).ToList();
            return Ok(dtos);
        }

        // POST: api/Testimonials
        [HttpPost]
        // [JwtAuthentication]
        public async Task<IHttpActionResult> PostTestimonial(Testimonial testimonial)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            db.Testimonials.Add(testimonial);
            await db.SaveChangesAsync();

            return Ok(testimonial);
        }

        // PUT: api/Testimonials/5
        [HttpPut]
        [JwtAuthentication]
        [Route("api/testimonials/{id}")]
        public async Task<IHttpActionResult> PutTestimonial(int id, Testimonial testimonial)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != testimonial.Id) return BadRequest();

            db.Entry(testimonial).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Testimonials/5
        [HttpDelete]
        [JwtAuthentication]
        [Route("api/testimonials/{id}")]
        public async Task<IHttpActionResult> DeleteTestimonial(int id)
        {
            Testimonial testimonial = await db.Testimonials.FindAsync(id);
            if (testimonial == null) return NotFound();

            db.Testimonials.Remove(testimonial);
            await db.SaveChangesAsync();

            return Ok(testimonial);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
