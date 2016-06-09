using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace YuukoBlog.Controllers
{
    public class PostController : BaseController
    {
        [Route("Post/{id}")]
        public IActionResult Post(string id)
        {
            var post = DB.Posts
                .Include(x => x.Catalog)
                .Include(x => x.Tags)
                .Where(x => x.Url == id && !x.IsPage)
                .SingleOrDefault();
            if (post == null)
                return Prompt(new Prompt
                {
                    StatusCode = 404,
                    Title = SR["Not Found"],
                    Details = SR["The resources have not been found, please check your request."],
                    RedirectUrl = Url.Link("default", new { controller = "Home", action = "Index" }),
                    RedirectText = SR["Back to home"]
                }); ViewBag.Title = post.Title;
            ViewBag.Position = post.CatalogId != null ? post.Catalog.Url : "home";
            return View(post);
        }

        [Route("{id}")]
        public IActionResult Page(string id)
        {
            var post = DB.Posts
                .Where(x => x.Url == id && x.IsPage)
                .SingleOrDefault();
            if (post == null)
                return Prompt(new Prompt
                {
                    StatusCode = 404,
                    Title = SR["Not Found"],
                    Details = SR["The resources have not been found, please check your request."],
                    RedirectUrl = Url.Link("default", new { controller = "Home", action = "Index" }),
                    RedirectText = SR["Back to home"]
                }); ViewBag.Title = post.Title;
            ViewBag.Position = post.CatalogId.HasValue ? post.Catalog.Url : "home";
            return View("Post", post);
        }
    }
}
