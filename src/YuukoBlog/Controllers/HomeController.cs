using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YuukoBlog.Models;

namespace YuukoBlog.Controllers
{
    public class HomeController : BaseController
    {
        [Route("{p:int?}")]
        public IActionResult Index(int p = 1)
        {
            return PagedView<PostViewModel, Post>(DB.Posts
                .Include(x => x.Catalog)
                .Include(x => x.Tags)
                .Where(x => !x.IsPage)
                .OrderByDescending(x => x.Time), 5, "Home");
        }

        [Route("{year:int}/{month:int}/{p:int?}")]
        public IActionResult Calendar(int year, int month, int p = 1)
        {
            var begin = new DateTime(year, month, 1);
            var end = begin.AddMonths(1);
            return PagedView<PostViewModel, Post>(DB.Posts
                .Include(x => x.Tags)
                .Include(x => x.Catalog)
                .Where(x => !x.IsPage)
                .Where(x => x.Time >= begin && x.Time <= end)
                .OrderByDescending(x => x.Time), 5, "Home");
        }

        [Route("Catalog/{id}/{p:int?}")]
        public IActionResult Catalog(string id, int p = 1)
        {
            var catalog = DB.Catalogs
                .Where(x => x.Url == id)
                .SingleOrDefault();
            if (catalog == null)
                return Prompt(x =>
                {
                    x.StatusCode = 404;
                    x.Title = SR["Not Found"];
                    x.Details = SR["The resources have not been found, please check your request."];
                    x.RedirectUrl = Url.Link("default", new { controller = "Home", action = "Index" });
                    x.RedirectText = SR["Back to home"];
                });
            ViewBag.Position = catalog.Url;
            return PagedView<PostViewModel, Post>(DB.Posts
                .Include(x => x.Tags)
                .Include(x => x.Catalog)
                .Where(x => !x.IsPage && x.CatalogId == catalog.Id)
                .OrderByDescending(x => x.Time), 5, "Home");
        }

        [Route("Tag/{tag}/{p:int?}")]
        public IActionResult Tag(string tag, int p = 1)
        {
            return PagedView<PostViewModel, Post>(DB.Posts
                 .Include(x => x.Tags)
                 .Include(x => x.Catalog)
                 .Where(x => !x.IsPage)
                 .Where(x => x.Tags.Any(y => y.Tag == tag))
                 .OrderByDescending(x => x.Time), 5, "Home");
        }

        [Route("Search/{id}/{p:int?}")]
        public IActionResult Search(string id, int p = 1)
        {
            return PagedView<PostViewModel, Post>(DB.Posts
                    .Include(x => x.Tags)
                    .Include(x => x.Catalog)
                    .Where(x => !x.IsPage)
                    .Where(x => x.Title.Contains(id) || id.Contains(x.Title))
                    .OrderByDescending(x => x.Time), 5, "Home");
        }

        public IActionResult Template(string Folder, [FromHeader] string Referer)
        {
            Cookies["ASPNET_TEMPLATE"] = Folder;
            return Redirect(Referer ?? "/");
        }
    }
}
