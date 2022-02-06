using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YuukoBlog.Models;
using YuukoBlog.Models.ViewModels;

namespace YuukoBlog.Controllers
{
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        [HttpGet("page/{page:int?}")]
        public async ValueTask<PagedApiResult<Post>> Get(
            [FromServices] BlogContext db,
            [FromQuery] string catalog = null,
            int page = 1,
            CancellationToken cancellationToken = default)
        {
            var query = db.Posts
                .Include(x => x.Catalog)
                .Include(x => x.Tags)
                .Where(x => !x.IsPage);

            if (!string.IsNullOrEmpty(catalog))
            {
                query = query.Where(x => x.Catalog.Url == catalog);
            }

            query = query
                .OrderByDescending(x => x.IsPinned)
                .ThenByDescending(x => x.Time);

            return await PagedApiResultAsync(query, page, 10, cancellationToken);
        }

        [HttpGet("{url}")]
        public async ValueTask<ApiResult<Post>> Get(
            [FromServices] BlogContext db,
            [FromRoute] string url,
            CancellationToken cancellationToken = default)
        {
            var post = await db.Posts.SingleOrDefaultAsync(x => x.Url == url, cancellationToken);
            if (post == null)
            {
                return ApiResult<Post>(404, "Post not found");
            }

            return ApiResult(post);
        }

        [HttpGet("calendar/{year:int}/{month:int}/{page:int?}")]
        public async ValueTask<PagedApiResult<Post>> Calendar(
            [FromServices] BlogContext db,
            int year, int month, int page = 1,
            CancellationToken cancellationToken = default)
        {
            var begin = new DateTime(year, month, 1);
            var end = begin.AddMonths(1);
            return await PagedApiResultAsync(db.Posts
                .Include(x => x.Tags)
                .Include(x => x.Catalog)
                .Where(x => !x.IsPage)
                .Where(x => x.Time >= begin && x.Time <= end)
                .OrderByDescending(x => x.IsPinned)
                .ThenByDescending(x => x.Time), page, 10, cancellationToken);
        }

        [HttpGet("catalog/{id}/{page:int?}")]
        public async ValueTask<PagedApiResult<Post>> Catalog(
            [FromServices] BlogContext db,
            string id, int page = 1,
            CancellationToken cancellationToken = default)
        {
            var catalog = db.Catalogs
                .Where(x => x.Url == id)
                .SingleOrDefault();

            if (catalog == null)
            {
                return PagedApiResult<Post>(404, "Catalog not found");
            }

            return await PagedApiResultAsync(db.Posts
                .Include(x => x.Tags)
                .Include(x => x.Catalog)
                .Where(x => !x.IsPage && x.CatalogId == catalog.Id)
                .OrderByDescending(x => x.IsPinned)
                .ThenByDescending(x => x.Time), page, 10, cancellationToken);
        }

        [HttpGet("tag/{tag}/{page:int?}")]
        public async ValueTask<PagedApiResult<Post>> Tag(
            [FromServices] BlogContext db,
            string tag, int page = 1,
            CancellationToken cancellationToken = default)
        {
            return await PagedApiResultAsync(db.Posts
                .Include(x => x.Tags)
                .Include(x => x.Catalog)
                .Where(x => !x.IsPage)
                .Where(x => x.Tags.Any(y => y.Tag == tag))
                .OrderByDescending(x => x.IsPinned)
                .ThenByDescending(x => x.Time), page, 10, cancellationToken);
        }

        [HttpGet("search/{id}/{page:int?}")]
        public async ValueTask<PagedApiResult<Post>> Search(
            [FromServices] BlogContext db,
            string id, int page = 1,
            CancellationToken cancellationToken = default)
        {
            return await PagedApiResultAsync(db.Posts
                .Include(x => x.Tags)
                .Include(x => x.Catalog)
                .Where(x => !x.IsPage)
                .Where(x => x.Title.Contains(id) || id.Contains(x.Title))
                .OrderByDescending(x => x.IsPinned)
                .ThenByDescending(x => x.Time), page, 10, cancellationToken);
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async ValueTask<ApiResult> PatchPost(
            [FromServices] BlogContext db,
            [FromRoute] Guid id,
            [FromBody] Post request,
            CancellationToken cancellationToken = default)
        {
            var post = await db.Posts
                .Include(x => x.Tags)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync(cancellationToken);

            if (post == null)
            {
                return ApiResult(404, "The post is not found");
            }

            var summary = "";
            var flag = false;
            if (request.Content != null)
            {
                var tmp = request.Content.Split('\n');
                if (tmp.Count() > 16)
                {
                    for (var i = 0; i < 16; i++)
                    {
                        if (tmp[i].IndexOf("```") == 0)
                            flag = !flag;
                        summary += tmp[i] + '\n';
                    }
                    if (flag)
                        summary += "```\r\n";
                    summary += $"\r\n[{"Read More"} »](/post/{request.Url})";
                }
                else
                {
                    summary = request.Content;
                }
            }

            foreach (var t in post.Tags)
            {
                db.PostTags.Remove(t);
            }

            post.Url = request.Url;
            post.Summary = summary;
            post.Title = request.Title;
            post.Content = request.Content;
            post.CatalogId = request.CatalogId;
            post.IsPage = request.IsPage;
            if (!string.IsNullOrEmpty(request.TagsText))
            {
                var _tags = request.TagsText.Split(',');
                foreach (var t in _tags)
                {
                    post.Tags.Add(new PostTag { PostId = post.Id, Tag = t.Trim(' ') });
                }
            }
            await db.SaveChangesAsync(cancellationToken);
            return ApiResult(200, "Succeeded");
        }

        [Authorize]
        [HttpPost]
        public async ValueTask<ApiResult<Post>> Post(
            [FromServices] BlogContext db, 
            CancellationToken cancellationToken = default)
        {
            var post = new Post
            {
                Id = Guid.NewGuid(),
                Url = Guid.NewGuid().ToString().Substring(0, 8),
                Title = "Untitled Post",
                Content = "",
                Summary = "",
                CatalogId = null,
                IsPage = false,
                Time = DateTime.Now
            };
            db.Posts.Add(post);
            await db.SaveChangesAsync(cancellationToken);
            return ApiResult(post);
        }
    }
}
