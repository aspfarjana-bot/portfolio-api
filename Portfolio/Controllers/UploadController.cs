using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Portfolio.Filters;

namespace Portfolio.Controllers
{
    public class UploadController : ApiController
    {
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".svg" };
        private const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5MB

        // POST: api/Upload/image
        [HttpPost]
        [JwtAuthentication]
        [Route("api/Upload/image")]
        public async Task<IHttpActionResult> UploadImage()
        {
            if (!Request.Content.IsMimeMultipartContent())
                return BadRequest("Unsupported media type. Please send multipart/form-data.");

            var uploadFolder = HttpContext.Current.Server.MapPath("~/Content/uploads");
            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            var provider = new MultipartFormDataStreamProvider(uploadFolder);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                var file = provider.FileData.FirstOrDefault();
                if (file == null)
                    return BadRequest("No file was uploaded.");

                // Validate file size
                var fileInfo = new FileInfo(file.LocalFileName);
                if (fileInfo.Length > MaxFileSizeBytes)
                {
                    File.Delete(file.LocalFileName);
                    return BadRequest("File size exceeds 5MB limit.");
                }

                // Get original filename & extension
                var originalName = file.Headers.ContentDisposition.FileName?.Trim('"') ?? "upload";
                var extension = Path.GetExtension(originalName).ToLowerInvariant();

                if (!AllowedExtensions.Contains(extension))
                {
                    File.Delete(file.LocalFileName);
                    return BadRequest($"File type '{extension}' is not allowed. Allowed: jpg, png, gif, webp, svg.");
                }

                // Rename to a unique name to avoid conflicts
                var uniqueName = $"{Guid.NewGuid()}{extension}";
                var finalPath = Path.Combine(uploadFolder, uniqueName);
                File.Move(file.LocalFileName, finalPath);

                // Build public URL
                var request = Request;
                var baseUrl = $"{request.RequestUri.Scheme}://{request.RequestUri.Authority}";
                var imageUrl = $"{baseUrl}/Content/uploads/{uniqueName}";

                return Ok(new { url = imageUrl, fileName = uniqueName });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
