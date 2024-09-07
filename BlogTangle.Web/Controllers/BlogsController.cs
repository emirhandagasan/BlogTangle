using BlogTangle.Web.Interfaces;
using BlogTangle.Web.Models.Domain;
using BlogTangle.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogTangle.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IBlogPostCommentRepository _blogPostCommentRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public BlogsController(IBlogPostRepository blogPostRepository,
            IBlogPostCommentRepository blogPostCommentRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _blogPostRepository = blogPostRepository;
            _blogPostCommentRepository = blogPostCommentRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPost = await _blogPostRepository.GetByUrlHandleAsync(urlHandle);

            var blogCommentsDomainModel = await _blogPostCommentRepository.GetCommentsByBlogId(blogPost.Id);
            var blogCommentsForView = new List<BlogCommentViewModel>();

            foreach(var blogComment in blogCommentsDomainModel)
            {
                var commentedUserId = await _userManager.FindByIdAsync(blogComment.UserId.ToString());

                if (commentedUserId != null)
                {
                    blogCommentsForView.Add(new BlogCommentViewModel
                    {
                        Description = blogComment.Description,
                        DateAdded = blogComment.DateAdded,
                        Username = commentedUserId.UserName
                    });
                }
                else
                {
                    await _blogPostCommentRepository.DeleteComment(blogComment.Id);
                }

            }
            
            

            var blogdetailsViewModel = new BlogDetailsViewModel
            {
                Id = blogPost.Id,
                Content = blogPost.Content,
                PageTitle = blogPost.PageTitle,
                Author = blogPost.Author,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                Heading = blogPost.Heading,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                UrlHandle = blogPost.UrlHandle,
                Visible = blogPost.Visible,
                Tags = blogPost.Tags,
                Comments = blogCommentsForView
                
            };

            return View(blogdetailsViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Index(BlogDetailsViewModel blogDetailsViewModel)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var domainModel = new BlogPostComment
                {
                    BlogPostId = blogDetailsViewModel.Id,
                    Description = blogDetailsViewModel.CommentDescription,
                    UserId = Guid.Parse(_userManager.GetUserId(User)),
                    DateAdded = DateTime.Now
                };

                await _blogPostCommentRepository.AddCommentAsync(domainModel);
                return RedirectToAction("Index", "Blogs",
                    new { urlHandle = blogDetailsViewModel.UrlHandle });
            }

            return View();
        }

    }
}
