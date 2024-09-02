using BlogTangle.Web.Data;
using BlogTangle.Web.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogTangle.Web.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _db;

        public UserRepository(AuthDbContext db)
        {
            _db = db;
        }


        public async Task<IEnumerable<IdentityUser>> GetAllUsersAsync()
        {
            var users = await _db.Users.ToListAsync();


            var superAdminUser = _db.Users
                .FirstOrDefault(x => x.Email == "superadmn@blogtangle.com");

            if(superAdminUser != null)
            {
                users.Remove(superAdminUser);
            }

            return users;
        }
    }
}
