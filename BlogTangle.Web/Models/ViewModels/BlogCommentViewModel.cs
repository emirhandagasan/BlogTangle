namespace BlogTangle.Web.Models.ViewModels
{
    public class BlogCommentViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string Username { get; set; }
    }
}
