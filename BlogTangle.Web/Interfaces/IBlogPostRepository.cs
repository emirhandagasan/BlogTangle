using BlogTangle.Web.Models.Domain;

namespace BlogTangle.Web.Interfaces
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllPostsAsync();
        Task<BlogPost?> GetPostAsync(Guid id);
        Task<BlogPost?> GetByUrlHandleAsync(string urlHandle);
        Task<int> AddPostAsync(BlogPost blogPost);
        Task<int?> UpdatePostAsync(BlogPost blogPost);
        Task<int?> DeletePostAsync(Guid id);
    }
}
