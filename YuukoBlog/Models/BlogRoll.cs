using System;
using System.ComponentModel.DataAnnotations;

namespace YuukoBlog.Models
{
    public class BlogRoll
    {
        public Guid Id { get; set; }

        [MaxLength(64)]
        public string Display { get; set; }

        [MaxLength(128)]
        public string Url { get; set; }

        public int Priority { get; set; }
    }
}
