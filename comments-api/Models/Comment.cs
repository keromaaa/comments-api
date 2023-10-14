namespace comments_api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Score { get; set; }
        public int Upvoted { get; set; }
        public List<Comment> Replies { get; set; } = new List<Comment>();
        public virtual int? ParentId { get; set; }

    }
}
