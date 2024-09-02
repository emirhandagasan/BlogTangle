using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogTangle.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed Roles

            var adminRoleId = "7f3020dc-b045-4975-a7d1-6d513e9d9b08";
            var superAdminRoleId = "72edffba-1297-4d66-bd83-5086c2509827";
            var userRoleId = "f51922ae-cc0f-45b5-9a7e-c89f26efcba2";


            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },

                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin".ToUpper(),
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },

                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User".ToUpper(),
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }

            };

            builder.Entity<IdentityRole>().HasData(roles);


            // Seed SuperAdminUser
            var superAdminId = "c0c2470f-5c64-4bde-870f-dc99e3b838cc";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmn@blogtangle.com",
                Email = "superadmn@blogtangle.com",
                NormalizedEmail = "superadmn@blogtangle.com".ToUpper(),
                NormalizedUserName = "superadmn@blogtangle.com".ToUpper(),
                Id = superAdminId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "Superadmn@123!");

            builder.Entity<IdentityUser>().HasData(superAdminUser);


            // Add all roles to SuperAdminUser
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                },

                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },

                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
