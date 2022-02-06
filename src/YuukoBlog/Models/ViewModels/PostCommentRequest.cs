namespace YuukoBlog.Models.ViewModels
{
    public class PostCommentRequest
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public Guid? ParentId { get; set; }
    }
}
