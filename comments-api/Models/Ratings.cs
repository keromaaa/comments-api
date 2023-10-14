namespace comments_api.Models
{
    public class Ratings
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Comment Comment { get; set; }
        public int Rating {  get; set; }
    }
}
