using Microsoft.EntityFrameworkCore;

namespace YuukoBlog.Models
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> opt)
            : base(opt)
        {
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostTag> PostTags { get; set; }

        public DbSet<Catalog> Catalogs { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Blob> Blobs { get; set; }

        public DbSet<BlogRoll> BlogRolls { get; set; }

        public DbSet<Link> Links { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Catalog>(e =>
            {
                e.HasIndex(x => x.Priority);
                e.HasMany(x => x.Posts).WithOne(x => x.Catalog).OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<Comment>(e =>
            {
                e.HasIndex(x => x.Time);
                e.HasMany(x => x.Comments).WithOne(x => x.Parent).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Post>(e =>
            {
                e.HasIndex(x => x.Title);
                e.HasIndex(x => x.IsPage);
                e.HasIndex(x => x.Time);
                e.HasIndex(x => x.Url).IsUnique();
                e.HasOne(x => x.Catalog).WithMany(x => x.Posts).IsRequired(false);
                e.HasMany(x => x.Comments).WithOne(x => x.Post).OnDelete(DeleteBehavior.Cascade);
                e.HasMany(x => x.Tags).WithOne(x => x.Post).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<PostTag>(e =>
            {
                e.HasIndex(x => x.Tag);
            });

            builder.Entity<BlogRoll>(e =>
            {
                e.HasIndex(x => x.Priority);
            });

            builder.Entity<Link>(e =>
            {
                e.HasIndex(x => x.Priority);
            });
        }
    }
}
