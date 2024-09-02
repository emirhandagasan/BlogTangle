using BlogTangle.Web.Interfaces;
using BlogTangle.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogTangle.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;


        public AdminUsersController(IUserRepository userRepository, 
            UserManager<IdentityUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await _userRepository.GetAllUsersAsync();

            var userListViewModel = new UserListViewModel();
            userListViewModel.Users = new List<UserViewModel>();

            foreach(var user in users)
            {
                userListViewModel.Users.Add(new UserViewModel
                {
                    Id = Guid.Parse(user.Id),
                    Username = user.UserName,
                    EmailAdress = user.Email
                });
            }

            return View(userListViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> List(UserListViewModel userListViewModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = userListViewModel.Username,
                Email = userListViewModel.Email
            };

            var identityResult = await _userManager.CreateAsync(identityUser, userListViewModel.Password);

            if(identityResult != null)
            {
                if (identityResult.Succeeded)
                {
                    var roles = new List<string> { "User" };

                    if (userListViewModel.AdminRoleCheckbox)
                    {
                        roles.Add("Admin");

                    }

                    

                    identityResult = await _userManager.AddToRolesAsync(identityUser, roles);

                    if(identityResult != null && identityResult.Succeeded)
                    {
                        return RedirectToAction("List", "AdminUsers");
                    }
                    
                }
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if(user != null)
            {
                var identityResult = await _userManager.DeleteAsync(user);

                if(identityResult != null && identityResult.Succeeded)
                {
                    return RedirectToAction("List", "AdminUsers");
                }
            }
            
            return View();
        }
    }
}
