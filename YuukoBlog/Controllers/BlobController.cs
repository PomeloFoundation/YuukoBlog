using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YuukoBlog.Models;

namespace YuukoBlog.Controllers
{
    public class BlobController : Controller
    {
        [HttpPost]
        public async ValueTask<IActionResult> Upload([FromServices] BlogContext db)
        {
            var file = HttpContext.Request.Form.Files["file"];
            if (file != null)
            {
                using (var stream = file.OpenReadStream())
                {
                    var f = new Blob
                    {
                        Time = DateTime.Now,
                        ContentType = file.ContentType,
                        ContentLength = file.Length,
                        FileName = file.FileName,
                        Bytes = ReadBytes(stream, (int)file.Length)
                    };
                    db.Blobs.Add(f);
                    await db.SaveChangesAsync();
                    return Json(f);
                }
            }
            else
            {
                var b64 = HttpContext.Request.Form["file"].ToString();
                var header = b64.Split(',')[0];
                var contentType = header.Split(':')[1].Split(';')[0];
                var content = b64.Substring(header.Length + 1);
                var bytes = Convert.FromBase64String(content);
                var f = new Blob
                {
                    Time = DateTime.Now,
                    ContentType = contentType,
                    ContentLength = bytes.Length,
                    FileName = "File",
                    Bytes = bytes
                };
                db.Blobs.Add(f);
                await db.SaveChangesAsync();
                return Json(f);
            }
        }

        [HttpGet("[controller]/[action]/{id}")]
        public async ValueTask<IActionResult> Download([FromServices] BlogContext db, Guid id)
        {
            HttpContext.Response.Headers["Cache-Control"] = $"max-age={ 60 * 24 }";
            var blob = await db.Blobs.SingleOrDefaultAsync(x => x.Id == id);
            if (blob == null)
            {
                return NotFound();
            }

            HttpContext.Response.ContentType = blob.ContentType;
            return File(blob.Bytes, blob.ContentType);
        }

        private byte[] ReadBytes(Stream stream, int length)
        {
            using (var reader = new BinaryReader(stream))
            {
                return reader.ReadBytes(length);
            }
        }
    }
}
