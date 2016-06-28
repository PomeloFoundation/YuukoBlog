using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pomelo.AspNetCore.Extensions.BlobStorage.Models;

namespace YuukoBlog.Models
{
    public enum BlogRollType
    {
        Following,
        Follower
    }

    public class BlogRoll
    {
        public Guid Id { get; set; }

        [MaxLength(64)]
        public string GitHubId { get; set; }

        [MaxLength(64)]
        public string NickName { get; set; }

        public BlogRollType Type { get; set; }

        [ForeignKey("Avatar")]
        public Guid? AvatarId { get; set; }

        public virtual Blob Avatar { get; set; }

        [MaxLength(128)]
        public string URL { get; set; }
    }
}
