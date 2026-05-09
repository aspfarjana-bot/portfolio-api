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
    public class ContactController : ApiController
    {
        private PortfolioDbContext db = new PortfolioDbContext();

        // GET: api/Contact
        [HttpGet]
        [JwtAuthentication]
        public async Task<IHttpActionResult> GetMessages()
        {
            var messages = await db.ContactMessages.OrderByDescending(m => m.CreatedAt).ToListAsync();
            var dtos = messages.Select(m => new ContactMessageDTO
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                Subject = m.Subject,
                Message = m.Message,
                CreatedAt = m.CreatedAt
            }).ToList();
            return Ok(dtos);
        }

        // POST: api/Contact
        [HttpPost]
        public async Task<IHttpActionResult> PostMessage(ContactMessage message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            message.CreatedAt = DateTime.Now;
            db.ContactMessages.Add(message);
            await db.SaveChangesAsync();

            return Ok(new { Success = true, Message = "Message received successfully." });
        }

        // DELETE: api/Contact/5
        [HttpDelete]
        [JwtAuthentication]
        public async Task<IHttpActionResult> DeleteMessage(int id)
        {
            var message = await db.ContactMessages.FindAsync(id);
            if (message == null) return NotFound();

            db.ContactMessages.Remove(message);
            await db.SaveChangesAsync();

            return Ok(new { Success = true });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
