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
    }
}
