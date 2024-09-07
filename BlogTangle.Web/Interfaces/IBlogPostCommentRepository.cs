using BlogTangle.Web.Models.Domain;

namespace BlogTangle.Web.Interfaces
{
    public interface IBlogPostCommentRepository
    {
        Task<int> AddCommentAsync(BlogPostComment blogPostComment);
        Task<IEnumerable<BlogPostComment>> GetCommentsByBlogId(Guid blogPostId);
        Task<int?> DeleteComment(Guid id);
    }
}
