using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YuukoBlog.Authentication;
using YuukoBlog.Models;
using YuukoBlog.Models.ViewModels;

namespace YuukoBlog.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        [HttpGet("session")]
        public ApiResult<bool> GetSession()
        {
            return ApiResult(User.Identity.IsAuthenticated);
        }

        [HttpPost("session")]
        public ApiResult<object> PostSession(
            [FromServices] IConfiguration configuration,
            [FromBody]LoginRequest request)
        {
            if (request.Username == configuration["Account"] && request.Password == configuration["Password"])
            {
                TokenAuthenticateHandler.Token = Guid.NewGuid().ToString().Replace("-", "");
                return ApiResult<object>(new 
                {
                    Token = TokenAuthenticateHandler.Token
                });
            }
            else
            {
                return ApiResult(400, "Username or password is incorrect.");
            }
        }

        [HttpPost("site")]
        public ApiResult PostSite(
            [FromServices] IConfiguration configuration,
            [FromBody] Config request)
        {
            if (string.IsNullOrEmpty(request.Account))
            {
                request.Account = configuration["Account"];
            }

            if (string.IsNullOrEmpty(request.Password))
            {
                request.Password = configuration["Password"];
            }

            System.IO.File.WriteAllText("appsettings.json", JsonConvert.SerializeObject(request));
            return ApiResult(200, "Succeeded");
        }

        /*
        [Authorize]
        [HttpPost]
        [Route("Admin/Post/New")]
        public IActionResult PostNew()
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
            DB.Posts.Add(post);
            DB.SaveChanges();
            return RedirectToAction("Post", "Post", new { id = post.Url });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Catalog()
        {
            return View(DB.Catalogs.OrderByDescending(x => x.Priority).ToList());
        }

        [Authorize]
        [HttpGet]
        [Route("Admin/Catalog/All")]
        public async ValueTask<IActionResult> CatalogAll(CancellationToken cancellationToken = default)
        {
            var catalogs = await DB.Catalogs
                .OrderBy(x => x.Priority)
                .ToListAsync(cancellationToken);
            return Json(catalogs);
        }

        [Authorize]
        [HttpPost]
        [Route("Admin/Catalog/Delete")]
        public IActionResult CatalogDelete(string id)
        {
            var catalog = DB.Catalogs.Where(x => x.Url == id).SingleOrDefault();
            if (catalog == null)
                return Prompt(x =>
                {
                    x.StatusCode = 404;
                    x.Title = "Not Found";
                    x.Details = "The resources have not been found, please check your request.";
                    x.RedirectUrl = Url.Link("default", new { controller = "Home", action = "Index" });
                    x.RedirectText = "Back to home";
                });
            DB.Catalogs.Remove(catalog);
            DB.SaveChanges();
            return Content("true");
        }

        [Authorize]
        [HttpPost]
        [Route("Admin/Catalog/Edit")]
        public IActionResult CatalogEdit(Guid id, string url, string title, int priority)
        {
            var catalog = DB.Catalogs.Where(x => x.Id == id).SingleOrDefault();
            if (catalog == null)
                return Prompt(x =>
                {
                    x.StatusCode = 404;
                    x.Title = "Not Found";
                    x.Details = "The resources have not been found, please check your request.";
                    x.RedirectUrl = Url.Link("default", new { controller = "Home", action = "Index" });
                    x.RedirectText = "Back to home";
                });
            catalog.Url = url;
            catalog.Title = title;
            catalog.Priority = priority;
            DB.SaveChanges();
            return Content("true");
        }

        [Authorize]
        [HttpPost]
        [Route("Admin/Catalog/New")]
        public IActionResult CatalogNew(Catalog model)
        {
            var catalog = new Catalog
            {
                Url = model.Url,
                Priority = model.Priority,
                Title = model.Title,
                Icon = model.Icon
            };
            DB.Catalogs.Add(catalog);
            DB.SaveChanges();
            return RedirectToAction("Catalog", "Admin");
        }

        [Authorize]
        [HttpPost]
        [Route("Admin/Delete/{id}")]
        public async ValueTask<IActionResult> Delete(Guid id)
        {
            var post = await DB.Posts.SingleAsync(x => x.Id == id);
            DB.Posts.Remove(post);
            await DB.SaveChangesAsync();
            return Content("Succeeded");
        }

        [Authorize]
        public IActionResult Link() => View();

        [Authorize]
        [HttpGet]
        [Route("Admin/Link/All")]
        public async ValueTask<IActionResult> GetLinks(CancellationToken cancellationToken = default)
        {
            var links = await DB.Links
                .OrderBy(x => x.Priority)
                .ToListAsync();

            return Json(links);
        }

        [Authorize]
        [HttpPost]
        [Route("Admin/Link/Delete")]
        public async ValueTask<IActionResult> DeleteLink(Guid id, CancellationToken cancellationToken = default)
        {
            var link = await DB.Links
                .SingleAsync(x => x.Id == id, cancellationToken);
            DB.Links.Remove(link);
            await DB.SaveChangesAsync();
            return Content("Succeeded");
        }

        [Authorize]
        [HttpPost]
        [Route("Admin/Link/Edit")]
        public async ValueTask<IActionResult> EditLink(
            Guid id, string display, string url, string icon,
            int priority, CancellationToken cancellationToken = default)
        {
            var link = await DB.Links
                .SingleAsync(x => x.Id == id, cancellationToken);
            link.Display = display;
            link.Url = url;
            link.Icon = icon;
            link.Priority = priority;
            await DB.SaveChangesAsync();
            return Content("Saved");
        }

        [Authorize]
        [HttpPost]
        [Route("Admin/Link/New")]
        public async ValueTask<IActionResult> NewLink(
            Link model, CancellationToken cancellationToken = default)
        {
            DB.Links.Add(model);
            await DB.SaveChangesAsync();
            return Content("Added");
        }


        [Authorize]
        public IActionResult BlogRoll() => View();

        [Authorize]
        [HttpGet]
        [Route("Admin/BlogRoll/All")]
        public async ValueTask<IActionResult> GetBlogRoll(CancellationToken cancellationToken = default)
        {
            var blogRolls = await DB.BlogRolls
                .OrderBy(x => x.Priority)
                .ToListAsync(cancellationToken);
            return Json(blogRolls);
        }

        [Authorize]
        [HttpPost]
        [Route("Admin/BlogRoll/New")]
        public async ValueTask<IActionResult> NewBlogRoll(
            BlogRoll model, CancellationToken cancellationToken = default)
        {
            DB.BlogRolls.Add(model);
            await DB.SaveChangesAsync();
            return Content("Added");
        }

        [Authorize]
        [HttpPost]
        [Route("Admin/BlogRoll/Delete")]
        public async ValueTask<IActionResult> DeleteBlogRoll(
            Guid id, CancellationToken cancellationToken = default)
        {
            var blogRoll = await DB.BlogRolls.SingleAsync(x => x.Id == id, cancellationToken);
            DB.BlogRolls.Remove(blogRoll);
            await DB.SaveChangesAsync();
            return Content("Succeeded");
        }

        [Authorize]
        [HttpPost]
        [Route("Admin/BlogRoll/Edit")]
        public async ValueTask<IActionResult> EditBlogRoll(
            Guid id, string display, string url, int priority, CancellationToken cancellationToken = default)
        {
            var blogRoll = await DB.BlogRolls.SingleAsync(x => x.Id == id, cancellationToken);
            blogRoll.Display = display;
            blogRoll.Priority = priority;
            blogRoll.URL = url;
            await DB.SaveChangesAsync();
            return Content("Succeeded");
        }

        [Authorize]
        [Route("Admin/Edit/{id}")]
        public IActionResult Edit(Guid id)
        {
            ViewBag.PostId = id;
            return View();
        }

        [Authorize]
        [HttpGet]
        [Route("Admin/Post/Body")]
        public async ValueTask<IActionResult> GetPost(Guid id, CancellationToken cancellationToken = default)
        {
            var post = await DB.Posts
                .Include(x => x.Tags)
                .SingleAsync(x => x.Id == id, cancellationToken);
            return Json(post);
        }

        [Authorize]
        [HttpPost]
        [Route("Admin/Post/Edit")]
        public IActionResult PostEdit(
            Guid id, string url, string tagsText, bool isPage,
            string title, Guid? catalogId, string content)
        {
            var post = DB.Posts
                .Include(x => x.Tags)
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (post == null)
                return Prompt(x =>
                {
                    x.StatusCode = 404;
                    x.Title = "Not Found";
                    x.Details = "The resources have not been found, please check your request.";
                    x.RedirectUrl = Url.Link("default", new { controller = "Home", action = "Index" });
                    x.RedirectText = "Back to home";
                });
            var summary = "";
            var flag = false;
            if (content != null)
            {
                var tmp = content.Split('\n');
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
                    summary += $"\r\n[{"Read More"} »](/post/{url})";
                }
                else
                {
                    summary = content;
                }
            }
            foreach (var t in post.Tags)
                DB.PostTags.Remove(t);
            post.Url = url;
            post.Summary = summary;
            post.Title = title;
            post.Content = content;
            post.CatalogId = catalogId;
            post.IsPage = isPage;
            if (!string.IsNullOrEmpty(tagsText))
            {
                var _tags = tagsText.Split(',');
                foreach (var t in _tags)
                    post.Tags.Add(new PostTag { PostId = post.Id, Tag = t.Trim(' ') });
            }
            DB.SaveChanges();
            return Content(Instance.Parse(content));
        }

        [Authorize]
        [HttpPost]
        [Route("Admin/Comment/Delete")]
        public async ValueTask<IActionResult> DeleteComment(
            Guid id, CancellationToken cancellationToken = default)
        {
            var comment = await DB.Comments
                .SingleAsync(x => x.Id == id, cancellationToken);
            DB.Comments.Remove(comment);
            await DB.SaveChangesAsync();
            return Content("Succeeded");
        }
        */
    }
}
