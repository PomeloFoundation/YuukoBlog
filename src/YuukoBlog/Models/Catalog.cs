using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YuukoBlog.Models
{
    public class Catalog
    {
        public Guid Id { get; set; }

        [MaxLength(32)]
        public string Url { get; set; }

        [MaxLength(64)]
        public string Title { get; set; }

        [MaxLength(16)]
        public string Icon { get; set; }

        public int Priority { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
