namespace comments_api.Models
{
    public class User
    {
        public int Id { get; set; }
        public byte[]? Image { get; set; }
        public string Username { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
