using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YuukoBlog.Models.ViewModels
{
    public class PatchPostRequest
    {
        public Guid Id { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string Content { get; set; }

        public DateTime Time { get; set; } = DateTime.Now;

        public bool IsPage { get; set; }

        public bool IsPinned { get; set; }

        public Guid? CatalogId { get; set; }

        [NotMapped]
        public string TagsText { get; set; }
    }
}
