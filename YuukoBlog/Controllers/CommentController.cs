using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YuukoBlog.Models;
using YuukoBlog.Models.ViewModels;

namespace YuukoBlog.Controllers
{
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        [HttpGet("{url}")]
        public async ValueTask<ApiResult<List<Comment>>> Get(
            [FromServices] BlogContext db,
            [FromRoute] string url, 
            CancellationToken cancellationToken = default)
        {
            var post = await db.Posts.SingleAsync(x => x.Url == url, cancellationToken);
            var comments = await db.Comments
                .Include(x => x.Comments)
                .Where(x => x.PostId == post.Id)
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

            return ApiResult(comments);
        }

        [HttpPost("{url}")]
        public async ValueTask<ApiResult<Comment>> Post(
            [FromServices] BlogContext db,
            [FromRoute] string url,
            [FromBody] PostCommentRequest request,
            CancellationToken cancellationToken = default)
        {
            var post = await db.Posts.SingleAsync(x => x.Url == url, cancellationToken);
            var comment = new Comment
            {
                Content = request.Content,
                Name = User.Identity.IsAuthenticated ? null : request.Name,
                Email = User.Identity.IsAuthenticated ? null : request.Email,
                Avatar = User.Identity.IsAuthenticated ? null : GetAvatarUrl(request.Email),
                ParentId = request.ParentId,
                PostId = post.Id,
                IsGuest = !User.Identity.IsAuthenticated
            };
            db.Comments.Add(comment);
            await db.SaveChangesAsync(cancellationToken);
            return ApiResult(comment);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async ValueTask<ApiResult> Post(
            [FromServices] BlogContext db,
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            var comment = await db.Comments.SingleAsync(x => x.Id == id, cancellationToken);
            db.Comments.Remove(comment);
            await db.SaveChangesAsync(cancellationToken);
            return ApiResult(200, "Comment deleted");
        }

        private static string GetAvatarUrl(string email)
        {
            using (var md5 = MD5.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(email.Trim().ToLower());
                var hash = BitConverter.ToString(md5.ComputeHash(bytes)).Replace("-", string.Empty).ToLower();
                return $"//cn.gravatar.org/avatar/{hash}?d=identicon";
            }
        }
    }
}
