using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace YuukoBlog.Models
{
    public static class SampleData
    {
        public static async Task InitializeYuukoBlog(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetService<BlogContext>();
            if (await db.Database.EnsureCreatedAsync())
            {
                db.Catalogs.Add(new Catalog
                {
                    Icon = "pencil",
                    Title = "Test",
                    Url = "test",
                    Priority = 0
                });

                db.Posts.Add(new Post
                {
                    Title = "test",
                    Url = "test",
                    Summary = "hello world",
                    Content = "hello world",
                    Time = DateTime.Now
                });

                db.Links.Add(new Link
                {
                    Display = "Github",
                    Url = "https://github.com",
                    Icon = "github"
                });

                await db.SaveChangesAsync();
            }
        }
    }
}
