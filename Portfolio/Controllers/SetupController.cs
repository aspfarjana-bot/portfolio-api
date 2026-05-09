using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Security.Cryptography;
using System.Text;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class SetupController : ApiController
    {
        private PortfolioDbContext db = new PortfolioDbContext();

        /// <summary>
        /// One-time setup: creates admin user if none exists.
        /// Call: POST /api/Setup/init
        /// After running once, you can remove or secure this endpoint.
        /// </summary>
        [HttpPost]
        [Route("api/Setup/init")]
        public async Task<IHttpActionResult> Init()
        {
            try
            {
                var admin = await db.AdminUsers.FirstOrDefaultAsync(u => u.Username == "admin");
                if (admin == null)
                {
                    admin = new AdminUser { Username = "admin", Role = "Admin" };
                    db.AdminUsers.Add(admin);
                }
                
                admin.PasswordHash = HashPassword("admin123");
                await db.SaveChangesAsync();

                return Ok(new { message = "✅ Admin user initialized/updated! Username: admin | Password: admin123" });
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException != null ? ex.InnerException.Message : "";
                return Ok(new { error = ex.Message, innerError = inner, stackTrace = ex.StackTrace });
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        [HttpGet]
        [Route("api/Setup/check")]
        public async Task<IHttpActionResult> Check()
        {
            try
            {
                int userCount = await db.AdminUsers.CountAsync();
                return Ok(new { userCount = userCount, message = "DB Connection successful" });
            }
            catch (Exception ex)
            {
                 return Ok(new { error = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
