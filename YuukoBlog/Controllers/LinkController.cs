using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YuukoBlog.Models;
using YuukoBlog.Models.ViewModels;

namespace YuukoBlog.Controllers
{
    [Route("api/[controller]")]
    public class LinkController : ControllerBase
    {
        [Authorize]
        [HttpPatch("{id:Guid}")]
        public async ValueTask<ApiResult> PatchLink(
            [FromServices] BlogContext db,
            [FromBody] Link link,
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            var _link = await db.Links
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (_link == null)
            {
                return ApiResult(404, "No link found");
            }

            _link.Url = link.Url;
            _link.Priority = link.Priority;
            _link.Icon = link.Icon;
            _link.Display = link.Display;

            await db.SaveChangesAsync(cancellationToken);
            return ApiResult(200, "Succeeded");
        }

        [Authorize]
        [HttpDelete("{id:Guid}")]
        public async ValueTask<ApiResult> DeleteLink(
            [FromServices] BlogContext db,
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            var _link = await db.Links
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (_link == null)
            {
                return ApiResult(404, "No link found");
            }

            db.Links.Remove(_link);
            await db.SaveChangesAsync(cancellationToken);
            return ApiResult(200, "Succeeded");
        }

        [Authorize]
        [HttpPost]
        public async ValueTask<ApiResult<Link>> PostLink(
            [FromServices] BlogContext db,
            [FromBody] Link link,
            CancellationToken cancellationToken = default)
        {
            db.Links.Add(link);
            await db.SaveChangesAsync(cancellationToken);
            return ApiResult(link);
        }
    }
}
