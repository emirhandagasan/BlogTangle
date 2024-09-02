using Microsoft.AspNetCore.Identity;

namespace BlogTangle.Web.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllUsersAsync();
    }
}
