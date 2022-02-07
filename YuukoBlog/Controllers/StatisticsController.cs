using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YuukoBlog.Models;
using YuukoBlog.Models.ViewModels;

namespace YuukoBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        [HttpGet("info")]
        public ApiResult<object> GetInfo(
            [FromServices] IConfiguration configuration)
        {
            return ApiResult<object>(new 
            {
                Site = configuration["Site"],
                Title = configuration["Site"],
                Name = configuration["Name"],
                AboutUrl = configuration["AboutUrl"],
                AvatarUrl = configuration["AvatarUrl"],
                Description = configuration["Description"]
            });
        }

        [HttpGet("tag")]
        public async ValueTask<ApiResult<List<TagViewModel>>> GetTags(
            [FromServices] BlogContext db,
            CancellationToken cancellationToken) 
        {
            var tags = await db.PostTags
                .OrderBy(x => x.Tag)
                .GroupBy(x => x.Tag)
                .Select(x => new TagViewModel
                {
                    Title = x.Key,
                    Count = x.Count()
                })
                .ToListAsync(cancellationToken);

            return ApiResult(tags);
        }

        [HttpGet("calendar")]
        public async ValueTask<ApiResult<List<CalendarViewModel>>> GetCalendars(
            [FromServices] BlogContext db,
            CancellationToken cancellationToken)
        {
            var calendars = await db.Posts
                .Where(x => !x.IsPage)
                .OrderByDescending(x => x.Time)
                .GroupBy(x => new { Year = x.Time.Year, Month = x.Time.Month })
                .Select(x => new CalendarViewModel
                {
                    Year = x.Key.Year,
                    Month = x.Key.Month,
                    Count = x.Count()
                })
                .ToListAsync(cancellationToken);

            return ApiResult(calendars);
        }

        [HttpGet("catalog")]
        public async ValueTask<ApiResult<List<CatalogViewModel>>> GetCatalogs(
            [FromServices] BlogContext db,
            CancellationToken cancellationToken)
        {
            var catalogs = await db.Catalogs
                .OrderByDescending(x => x.Priority)
                .Select(x => new CatalogViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Count = db.Posts.Where(y => y.CatalogId == x.Id).Count(),
                    Priority = x.Priority,
                    Url = x.Url,
                    Icon = x.Icon
                })
                .ToListAsync(cancellationToken);

            return ApiResult(catalogs);
        }

        [HttpGet("roll")]
        public async ValueTask<ApiResult<List<BlogRollViewModel>>> GetRolls(
            [FromServices] BlogContext db,
            CancellationToken cancellationToken)
        {
            var rolls = await db.BlogRolls
                .OrderBy(x => x.Priority)
                .Select(x => new BlogRollViewModel
                {
                    Id = x.Id,
                    Display = x.Display,
                    Url = x.Url,
                    Priority = x.Priority
                })
                .ToListAsync(cancellationToken); 

            return ApiResult(rolls);
        }

        [HttpGet("link")]
        public async ValueTask<ApiResult<List<Link>>> GetLinks(
            [FromServices] BlogContext db,
            CancellationToken cancellationToken)
        {
            var links = await db.Links
                .OrderBy(x => x.Priority)
                .ToListAsync(cancellationToken);

            return ApiResult(links);
        }
    }
}
