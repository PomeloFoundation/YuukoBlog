using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YuukoBlog.Models
{
    public class Comment
    {
        public Guid Id { get; set; }

        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }

        public virtual Post Post { get; set; }

        [MaxLength(256)]
        public string Email { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }

        public bool IsGuest { get; set; }

        public string Content { get; set; }

        [ForeignKey(nameof(Parent))]
        public Guid? ParentId { get; set; }

        public virtual Comment Parent { get; set; }
    }
}
