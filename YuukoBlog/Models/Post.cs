using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace YuukoBlog.Models
{
    public class Post
    {
        public Guid Id { get; set; }

        [MaxLength(256)]
        public string Url { get; set; }

        [MaxLength(256)]
        public string Title { get; set; }

        public string Summary { get; set; }

        public string Content { get; set; }

        public DateTime Time { get; set; } = DateTime.Now;

        public bool IsPage { get; set; }

        public bool IsPinned { get; set; }

        [ForeignKey("Catalog")]
        public Guid? CatalogId { get; set; }

        public virtual Catalog Catalog { get; set; }

        public virtual ICollection<PostTag> Tags { get; set; } = new List<PostTag>();

        [NotMapped]
        public string TagsText
        {
            get { return string.Join(",", Tags.Select(x => x.Tag)); }
        }

        [JsonIgnore]
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
