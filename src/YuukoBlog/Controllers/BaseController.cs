using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.AspNetCore.Extensions.Localization;
using YuukoBlog.Models;

namespace YuukoBlog.Controllers
{
    public class BaseController : BaseController<BlogContext>
    {
        [Inject]
        public ILocalizationStringCollection SR { get; set; }

        public override void Prepare()
        {
            base.Prepare();

            // Building Constants
            ViewBag.Position = "home";
            ViewBag.IsPost = false;
            ViewBag.Description = Configuration["Description"];
            ViewBag.Title = Configuration["Site"];
            ViewBag.Site = Configuration["Site"];
            ViewBag.AboutUrl = Configuration["AboutUrl"];
            ViewBag.AvatarUrl = Configuration["AvatarUrl"];
            ViewBag.Disqus = Configuration["Disqus"];
            ViewBag.Account = Configuration["Account"];
            ViewBag.DefaultTemplate = Configuration["DefaultTemplate"];

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
                .OrderByDescending(x => x.PRI)
                .ToList()
                .Select(x => new CatalogViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Count = x.Posts.Count(),
                    PRI = x.PRI,
                    Url = x.Url
                })
                .ToList();
        }
    }
}
