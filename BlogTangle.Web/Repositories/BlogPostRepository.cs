using BlogTangle.Web.Data;
using BlogTangle.Web.Interfaces;
using BlogTangle.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogTangle.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext _db;
        public BlogPostRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<int> AddPostAsync(BlogPost blogPost)
        {
            await _db.AddAsync(blogPost);
            return await _db.SaveChangesAsync();
        }

        public async Task<int?> DeletePostAsync(Guid id)
        {
            var existingPost = await GetPostAsync(id);

            if(existingPost != null)
            {
                _db.BlogPosts.Remove(existingPost);
                return await _db.SaveChangesAsync();
            }

            return null;
            
        }

        public async Task<IEnumerable<BlogPost>> GetAllPostsAsync()
        {
            return await _db.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost?> GetPostAsync(Guid id)
        {
            return await _db.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<int?> UpdatePostAsync(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }
    }
}
