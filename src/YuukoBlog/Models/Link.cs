using System;
using System.ComponentModel.DataAnnotations;

namespace YuukoBlog.Models
{
    public class Link
    {
        public Guid Id { get; set; }

        [MaxLength(256)]
        public string Url { get; set; }

        [MaxLength(32)]
        public string Display { get; set; }

        public int Priority { get; set; }

        [MaxLength(16)]
        public string Icon { get; set; }
    }
}
