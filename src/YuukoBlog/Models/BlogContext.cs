using Microsoft.EntityFrameworkCore;
using Pomelo.AspNetCore.Extensions.BlobStorage.Models;

namespace YuukoBlog.Models
{
    public class BlogContext : DbContext, IBlobStorageDbContext
    {
        public BlogContext(DbContextOptions<BlogContext> opt)
            : base(opt)
        {
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostTag> PostTags { get; set; }

        public DbSet<Catalog> Catalogs { get; set; }

        public DbSet<Blob> Blobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.SetupBlobStorage();

            modelBuilder.Entity<Catalog>(e =>
            {
                e.HasIndex(x => x.PRI);
            });

            modelBuilder.Entity<Post>(e =>
            {
                e.HasIndex(x => x.IsPage);
                e.HasIndex(x => x.Time);
                e.HasIndex(x => x.Url).IsUnique();
            });

            modelBuilder.Entity<PostTag>(e =>
            {
                e.HasIndex(x => x.Tag);
            });
        }
    }
}
