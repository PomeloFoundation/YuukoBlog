using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YuukoBlog.Models;
using YuukoBlog.Models.ViewModels;

namespace YuukoBlog.Controllers
{
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        [Authorize]
        [HttpPatch("{id:Guid}")]
        public async ValueTask<ApiResult> PatchCatalog(
            [FromServices] BlogContext db,
            [FromBody] Catalog catalog,
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            var _catalog = await db.Catalogs
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (_catalog == null)
            {
                return ApiResult(404, "No catalog found");
            }

            _catalog.Url = catalog.Url;
            _catalog.Priority = catalog.Priority;
            _catalog.Icon = catalog.Icon;
            _catalog.Title = catalog.Title;

            await db.SaveChangesAsync(cancellationToken);
            return ApiResult(200, "Succeeded");
        }

        [Authorize]
        [HttpDelete("{id:Guid}")]
        public async ValueTask<ApiResult> DeleteCatalog(
            [FromServices] BlogContext db,
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            var _catalog = await db.Catalogs
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (_catalog == null)
            {
                return ApiResult(404, "No catalog found");
            }

            db.Catalogs.Remove(_catalog);
            await db.SaveChangesAsync(cancellationToken);
            return ApiResult(200, "Succeeded");
        }

        [Authorize]
        [HttpPost]
        public async ValueTask<ApiResult<Catalog>> PostCatalog(
            [FromServices] BlogContext db,
            [FromBody] Catalog catalog,
            CancellationToken cancellationToken = default)
        {
            var _catalog = await db.Catalogs
                .SingleOrDefaultAsync(x => x.Url == catalog.Url, cancellationToken);

            if (_catalog != null)
            {
                return ApiResult<Catalog>(400, "URL already exists");
            }

            db.Catalogs.Add(catalog);
            await db.SaveChangesAsync(cancellationToken);
            return ApiResult(catalog);
        }
    }
}
