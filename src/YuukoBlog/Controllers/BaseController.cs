using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YuukoBlog.Models;
using YuukoBlog.Utils;
using YuukoBlog.Utils.Pagination;

namespace YuukoBlog.Controllers
{
    public class BaseController : Controller
    {
        private BlogContext _db;
        public BlogContext DB
        {
            get
            {
                if (_db == null)
                {
                    _db = HttpContext.RequestServices.GetRequiredService<BlogContext>();
                }
                return _db;
            }
        }

        private IConfiguration _configuration;
        public IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
                }
                return _configuration;
            }
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Prepare();
            base.OnActionExecuting(context);
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Prepare();
            return base.OnActionExecutionAsync(context, next);
        }


        [NonAction]
        protected IActionResult Prompt(Action<Prompt> setupPrompt)
        {
            var prompt = new Prompt();
            setupPrompt(prompt);
            Response.StatusCode = prompt.StatusCode;
            return View("Prompt", prompt);
        }

        [NonAction]
        protected IActionResult PagedView<TModel>(
            IEnumerable<TModel> Source,
            int PageSize = 50,
            string ViewPath = null)
        {
            int? p;
            try
            {
                if (Request.Query["p"].Count > 0)
                {
                    p = int.Parse(Request.Query["p"].ToString());
                }
                else if (RouteData.Values["p"] != null)
                {
                    p = int.Parse(RouteData.Values["p"].ToString());
                }
                else
                {
                    p = 1;
                }
            }
            catch
            {
                p = 1;
            }
            ViewData["PagingInfo"] = Paging.Divide(ref Source, PageSize, p.Value);
            if (string.IsNullOrEmpty(ViewPath))
                return View(Source);
            else
                return View(ViewPath, Source);
        }

        [NonAction]
        protected IActionResult PagedView<TView, TModel>(
          IEnumerable<TModel> Source,
          int PageSize = 50,
          string ViewPath = null)
          where TView : class
          where TModel : IConvertible<TView>
        {
            int? p;
            try
            {
                if (Request.Query["p"].Count > 0)
                {
                    p = int.Parse(Request.Query["p"].ToString());
                }
                else if (RouteData.Values["p"] != null)
                {
                    p = int.Parse(RouteData.Values["p"].ToString());
                }
                else
                {
                    p = 1;
                }
            }
            catch
            {
                p = 1;
            }
            ViewData["PagingInfo"] = Paging.Divide(ref Source, PageSize, p.Value);
            var ret = new List<TView>();
            foreach (var item in Source)
            {
                var tmp = (item as IConvertible<TView>).ToType();
                ret.Add(tmp);
            }
            if (string.IsNullOrEmpty(ViewPath))
                return View(ret);
            else
                return View(ViewPath, ret);
        }

        public void Prepare()
        {
            // Building Constants
            ViewBag.Position = "home";
            ViewBag.IsPost = false;
            ViewBag.Description = Configuration["Description"];
            ViewBag.Title = Configuration["Site"];
            ViewBag.Site = Configuration["Site"];
            ViewBag.Name = Configuration["Name"];
            ViewBag.AboutUrl = Configuration["AboutUrl"];
            ViewBag.AvatarUrl = Configuration["AvatarUrl"];
            ViewBag.Account = Configuration["Account"];

            // Building Tags
            ViewBag.Tags = DB.PostTags
                .OrderBy(x => x.Tag)
                .GroupBy(x => x.Tag)
                .Select(x => new TagViewModel
                {
                    Title = x.Key,
                    Count = x.Count()
                })
                .ToList();

            // Building Calendar
            ViewBag.Calendars = DB.Posts
                .Where(x => !x.IsPage)
                .OrderByDescending(x => x.Time)
                .GroupBy(x => new { Year = x.Time.Year, Month = x.Time.Month })
                .Select(x => new CalendarViewModel
                {
                    Year = x.Key.Year,
                    Month = x.Key.Month,
                    Count = x.Count()
                })
                .ToList();

            // Building Catalogs
            ViewBag.Catalogs = DB.Catalogs
                .Include(x => x.Posts)
                .OrderByDescending(x => x.Priority)
                .ToList()
                .Select(x => new CatalogViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Count = x.Posts.Count(),
                    Priority = x.Priority,
                    Url = x.Url,
                    Icon = x.Icon
                })
                .ToList();

            // Building Blog Rolls
            var rolls = DB.BlogRolls
                .OrderBy(x => x.Priority)
                .Select(x => new BlogRollViewModel
                {
                    Display = x.Display,
                    URL = x.URL
                })
                .ToList();
            ViewBag.Rolls = rolls;

            // Binding Links
            ViewBag.Links = DB.Links
                .OrderBy(x => x.Priority)
                .ToList();
        }
    }
}
