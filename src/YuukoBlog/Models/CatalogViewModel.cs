using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YuukoBlog.Models
{
    public class CatalogViewModel
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public int PRI { get; set; }
    }
}
