using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace YuukoBlog.Models
{
    public static class SampleData
    {
        public static async Task InitializeYuukoBlog(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetService<BlogContext>();
            await db.Database.EnsureCreatedAsync();
        }
    }
}
