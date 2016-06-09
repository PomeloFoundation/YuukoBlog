using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YuukoBlog.Models
{
    public class Config
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string Site { get; set; }
        public string Description { get; set; }
        public string Disqus { get; set; }
        public string AvatarUrl { get; set; }
        public string AboutUrl { get; set; }
    }
}
