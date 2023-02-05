namespace FineBlog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        //Table relation One To One
        public string? AapplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string? Description { get; set; }
        public string? Slug { get; set; }
    }
}
