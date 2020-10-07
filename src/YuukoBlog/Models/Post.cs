using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YuukoBlog.Utils;

namespace YuukoBlog.Models
{
    public class Post : IConvertible<PostViewModel>
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

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        PostViewModel IConvertible<PostViewModel>.ToType()
        {
            return new PostViewModel
            {
                Id = Id,
                Summary = Summary,
                Catalog = Catalog,
                CatalogId = CatalogId,
                Tags = Tags.ToList(),
                Time = Time,
                Title = Title,
                Url = Url
            };
        }
    }
}
