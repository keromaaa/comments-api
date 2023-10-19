namespace comments_api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int? CommentId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Score { get; set; }
        public int Upvoted { get; set; }
        public List<Comment> Replies { get; set; } = new List<Comment>();
        public virtual int? ParentId { get; set; }

        public Comment()
        {
            CreatedAt = DateTime.UtcNow;
        }

    }
}
