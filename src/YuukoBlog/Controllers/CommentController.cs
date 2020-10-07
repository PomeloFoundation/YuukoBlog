using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YuukoBlog.Models;

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

            if (!User.Identity.IsAuthenticated)
            {
                foreach (var x in comments)
                {
                    x.Email = null;
                    x.Content = Pomelo.AntiXSS.Instance.Sanitize(Pomelo.Marked.Instance.Parse(x.Content));
                    foreach (var y in x.Comments)
                    {
                        x.Email = null;
                        y.Content = Pomelo.AntiXSS.Instance.Sanitize(Pomelo.Marked.Instance.Parse(y.Content));
                    }
                }
            }

            return Json(comments);
        }

        [HttpPost("/comment/{id}")]
        public async ValueTask<IActionResult> Post(
            Guid id,
            string content,
            string name = null,
            string email = null,
            Guid? parentId = null,
            CancellationToken cancellationToken = default)
        {
            var comment = new Comment
            {
                Content = content,
                Name = User.Identity.IsAuthenticated ? null : name,
                Email = User.Identity.IsAuthenticated ? null : email,
                Avatar = GetAvatarUrl(email),
                ParentId = parentId,
                PostId = id,
                IsGuest = !User.Identity.IsAuthenticated
            };
            DB.Comments.Add(comment);
            await DB.SaveChangesAsync(cancellationToken);
            return Content("Succeeded");
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
