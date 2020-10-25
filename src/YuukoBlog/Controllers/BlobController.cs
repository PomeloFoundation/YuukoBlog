using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pomelo.Marked;
using YuukoBlog.Filters;
using YuukoBlog.Models;
using System.IO;
using System.Threading.Tasks;

namespace YuukoBlog.Controllers
{
    public class BlobController : BaseController
    {
        [HttpPost]
        public async ValueTask<IActionResult> Upload()
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
                    DB.Blobs.Add(f);
                    await DB.SaveChangesAsync();
                    return Json(f);
                }
            }
            else
            {
                var b64 = HttpContext.Request.Form["file"].ToString();
                var bytes = Convert.FromBase64String(b64);
                var f = new Blob
                {
                    Time = DateTime.Now,
                    ContentType = "application/octet-stream",
                    ContentLength = bytes.Length,
                    FileName = "File",
                    Bytes = bytes
                };
                DB.Blobs.Add(f);
                await DB.SaveChangesAsync();
                return Json(f);
            }
        }

        public async ValueTask<IActionResult> Download(Guid id)
        {
            HttpContext.Response.Headers["Cache-Control"] = $"max-age={ 60 * 24 }";
            var blob = await DB.Blobs.SingleOrDefaultAsync(x => x.Id == id);
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
