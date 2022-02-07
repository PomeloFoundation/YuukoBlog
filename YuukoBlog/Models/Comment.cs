using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace YuukoBlog.Models
{
    public class Comment
    {
        public Guid Id { get; set; }

        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }

        [JsonIgnore]
        public virtual Post Post { get; set; }

        [MaxLength(256)]
        public string Email { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }

        public bool IsGuest { get; set; }

        public string Content { get; set; }

        [MaxLength(256)]
        public string Avatar { get; set; }

        public DateTime Time { get; set; } = DateTime.Now;

        [ForeignKey(nameof(Parent))]
        public Guid? ParentId { get; set; }

        [JsonIgnore]
        public virtual Comment Parent { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
