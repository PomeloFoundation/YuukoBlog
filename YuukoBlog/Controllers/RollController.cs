using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YuukoBlog.Models;
using YuukoBlog.Models.ViewModels;

namespace YuukoBlog.Controllers
{
    [Route("api/[controller]")]
    public class RollController : ControllerBase
    {
        [Authorize]
        [HttpPatch("{id:Guid}")]
        public async ValueTask<ApiResult> PatchRoll(
            [FromServices] BlogContext db,
            [FromBody] BlogRoll roll,
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            var _roll = await db.BlogRolls
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (_roll == null)
            {
                return ApiResult(404, "No blog roll found");
            }

            _roll.Url = roll.Url;
            _roll.Priority = roll.Priority;
            _roll.Display = roll.Display;

            await db.SaveChangesAsync(cancellationToken);
            return ApiResult(200, "Succeeded");
        }

        [Authorize]
        [HttpDelete("{id:Guid}")]
        public async ValueTask<ApiResult> DeleteRoll(
            [FromServices] BlogContext db,
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            var _roll = await db.BlogRolls
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (_roll == null)
            {
                return ApiResult(404, "No blog roll found");
            }

            db.BlogRolls.Remove(_roll);
            await db.SaveChangesAsync(cancellationToken);
            return ApiResult(200, "Succeeded");
        }

        [Authorize]
        [HttpPost]
        public async ValueTask<ApiResult<BlogRoll>> PostLink(
            [FromServices] BlogContext db,
            [FromBody] BlogRoll roll,
            CancellationToken cancellationToken = default)
        {
            db.BlogRolls.Add(roll);
            await db.SaveChangesAsync(cancellationToken);
            return ApiResult(roll);
        }
    }
}
