using BlogTangle.Web.Interfaces;
using BlogTangle.Web.Models;
using BlogTangle.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogTangle.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ITagRepository _tagRepository;

        public HomeController(ILogger<HomeController> logger, 
                              IBlogPostRepository blogPostRepository, 
                              ITagRepository tagRepository)
        {
            _logger = logger;
            _blogPostRepository = blogPostRepository;
            _tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index()
        {
            var blogPosts = await _blogPostRepository.GetAllPostsAsync();

            var tags = await _tagRepository.GetAllTagsAsync();


            var model = new HomeViewModel
            {
                BlogPosts = blogPosts,
                Tags = tags
            };

            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
