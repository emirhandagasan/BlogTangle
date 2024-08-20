using BlogTangle.Web.Models.Domain;

namespace BlogTangle.Web.Interfaces
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllTagsAsync();
        Task<Tag?> GetTagAsync(Guid id);
        Task<int> AddTagAsync(Tag tag);
        Task<int?> UpdateTagAsync(Tag tag);
        Task<int?> DeleteTagAsync(Guid id);
    }
}
