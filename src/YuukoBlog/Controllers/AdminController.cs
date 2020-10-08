using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pomelo.Marked;
using YuukoBlog.Filters;
using YuukoBlog.Models;

namespace YuukoBlog.Controllers
{
    public class AdminController : BaseController
    {
        [Authorize]
        [HttpGet]
        [Route("Admin/Index")]
        public IActionResult Index() 
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [Route("Admin/Index")]
        public IActionResult Index(Config config)
        {
            Configuration["Account"] = config.Account;
            if (string.IsNullOrEmpty(config.Password))
            {
                config.Password = Configuration["Password"];
            }
            Configuration["Password"] = config.Password;
            Configuration["Site"] = config.Site;
            Configuration["Description"] = config.Description;
            Configuration["Name"] = config.Name;
            System.IO.File.WriteAllText("appsettings.json", JsonConvert.SerializeObject(config));
            return RedirectToAction("Index", "Admin");
        }

        [GuestRequired]
        public IActionResult Login()
        {
            return View();
        }

        [GuestRequired]
        [HttpPost]
        public IActionResult Login(string Username, string Password)
        {
            var tmp = Configuration["Account"];
            if (Username == Configuration["Account"] && Password == Configuration["Password"])
            {
                HttpContext.Session.SetString("Admin", "true");
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        [HttpPost]
        [Route("Admin/Post/Edit")]
        public IActionResult PostEdit(string id, string newId, string tags, bool isPage, string title, Guid? catalog, string content)
        {
            var post = DB.Posts
                .Include(x => x.Tags)
                .Where(x => x.Url == id)
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
                    summary += $"\r\n[{"Read More"} »](/post/{newId})";
                }
                else
                {
                    summary = content;
                }
            }
            foreach (var t in post.Tags)
                DB.PostTags.Remove(t);
            post.Url = newId;
            post.Summary = summary;
            post.Title = title;
            post.Content = content;
            post.CatalogId = catalog;
            post.IsPage = isPage;
            if (!string.IsNullOrEmpty(tags))
            { 
                var _tags = tags.Split(',');
                foreach (var t in _tags)
                    post.Tags.Add(new PostTag { PostId = post.Id, Tag = t.Trim(' ') });
            }
            DB.SaveChanges();
            return Content(Instance.Parse(content));
        }

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
    }
}
