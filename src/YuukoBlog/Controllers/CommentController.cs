using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace YuukoBlog.Controllers
{
    public class CommentController : BaseController
    {
        [HttpGet("/comment/{id}")]
        public async ValueTask<IActionResult> Get(Guid id, CancellationToken cancellationToken = default)
        {
            var comments = await DB.Comments
                .Include(x => x.Comments)
                .Where(x => x.PostId == id)
                .Where(x => !x.ParentId.HasValue)
                .OrderBy(x => x.Time)
                .ToListAsync(cancellationToken);
        }

        private static string GetAvatarUrl(string email)
        {
            using (var md5 = MD5.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(email.Trim().ToLower());
                var hash = BitConverter.ToString(md5.ComputeHash(bytes)).Replace("-", string.Empty).ToLower();
                return $"//www.gravatar.com/avatar/{hash}?d=identicon";
            }
        }
    }
}
