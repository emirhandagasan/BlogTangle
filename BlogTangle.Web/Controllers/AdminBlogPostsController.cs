using BlogTangle.Web.Interfaces;
using BlogTangle.Web.Models.Domain;
using BlogTangle.Web.Models.ViewModels;
using BlogTangle.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogTangle.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IBlogPostRepository _blogPostRepository;
        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            _tagRepository = tagRepository;
            _blogPostRepository = blogPostRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // get tags from repository
            var tags = await _tagRepository.GetAllTagsAsync();

            var addBlogPostViewModel = new AddBlogPostViewModel
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString() })
            };

            return View(addBlogPostViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostViewModel addblogPostViewModel)
        {
            var blogPost = new BlogPost
            {
                Heading = addblogPostViewModel.Heading,
                PageTitle = addblogPostViewModel.PageTitle,
                Content = addblogPostViewModel.Content,
                ShortDescription = addblogPostViewModel.ShortDescription,
                FeaturedImageUrl = addblogPostViewModel.FeaturedImageUrl,
                UrlHandle = addblogPostViewModel.UrlHandle,
                PublishedDate = addblogPostViewModel.PublishedDate,
                Author = addblogPostViewModel.Author,
                Visible = addblogPostViewModel.Visible,
            };

            // Mapping Tags

            var selectedTags = new List<Tag>();
            foreach (var selectedTagId in addblogPostViewModel.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await _tagRepository.GetTagAsync(selectedTagIdAsGuid);

                if(existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }

            blogPost.Tags = selectedTags;

            await _blogPostRepository.AddPostAsync(blogPost);
            
            return View();
        }
    }
}
