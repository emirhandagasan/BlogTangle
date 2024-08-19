using BlogTangle.Web.Data;
using BlogTangle.Web.Interfaces;
using BlogTangle.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogTangle.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _db;
        public TagRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<int> AddTagAsync(Tag tag)
        {
            await _db.Tags.AddAsync(tag);
            return await _db.SaveChangesAsync();
            
        }

        public async Task<int?> DeleteTagAsync(Guid id)
        {
            var existingTag = await GetTagAsync(id);

            if(existingTag != null)
            {
                _db.Tags.Remove(existingTag);
                return await _db.SaveChangesAsync();
            }

            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            return await _db.Tags.ToListAsync();
        }

        public async Task<Tag?> GetTagAsync(Guid id)
        {
            return await _db.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int?> UpdateTagAsync(Tag tag)
        {
            var existingTag = await GetTagAsync(tag.Id);

            if(existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                return await _db.SaveChangesAsync();
            }

            return null;
        }
    }
}
