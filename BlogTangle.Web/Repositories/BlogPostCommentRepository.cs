using BlogTangle.Web.Data;
using BlogTangle.Web.Interfaces;
using BlogTangle.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogTangle.Web.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly ApplicationDbContext _db;

        public BlogPostCommentRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<int> AddCommentAsync(BlogPostComment blogPostComment)
        {
            await _db.BlogPostComments.AddAsync(blogPostComment);
            return await _db.SaveChangesAsync();
        }

        public async Task<int?> DeleteComment(Guid id)
        {
            var existingComment = await _db.BlogPostComments.FirstOrDefaultAsync(x => x.Id == id);

            if(existingComment != null)
            {
                _db.Remove(existingComment);
                return await _db.SaveChangesAsync();
            }

            return null;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentsByBlogId(Guid blogPostId)
        {
            return await _db.BlogPostComments.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }
    }
}
