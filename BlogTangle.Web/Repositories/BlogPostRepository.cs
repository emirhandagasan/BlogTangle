using BlogTangle.Web.Data;
using BlogTangle.Web.Interfaces;
using BlogTangle.Web.Models.Domain;
using BlogTangle.Web.Models.ViewModels;
using Microsoft.AspNetCore.Components.Web;
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
            return await _db.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetPostAsync(Guid id)
        {
            return await _db.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int?> UpdatePostAsync(BlogPost blogPost)
        {
            var existingPost = await GetPostAsync(blogPost.Id);
            
            if(existingPost != null)
            {
                existingPost.Heading = blogPost.Heading;
                existingPost.PageTitle = blogPost.PageTitle;
                existingPost.Content = blogPost.Content;
                existingPost.ShortDescription = blogPost.ShortDescription;
                existingPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingPost.UrlHandle = blogPost.UrlHandle;
                existingPost.PublishedDate = blogPost.PublishedDate;
                existingPost.Author = blogPost.Author;
                existingPost.Visible = blogPost.Visible;
                existingPost.Tags = blogPost.Tags;

                return await _db.SaveChangesAsync();
            }

            return null;
        }
    }
}
