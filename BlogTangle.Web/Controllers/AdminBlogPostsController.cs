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
        public async Task<IActionResult> Add(AddBlogPostViewModel addBlogPostViewModel)
        {
            var blogPost = new BlogPost
            {
                Heading = addBlogPostViewModel.Heading,
                PageTitle = addBlogPostViewModel.PageTitle,
                Content = addBlogPostViewModel.Content,
                ShortDescription = addBlogPostViewModel.ShortDescription,
                FeaturedImageUrl = addBlogPostViewModel.FeaturedImageUrl,
                UrlHandle = addBlogPostViewModel.UrlHandle,
                PublishedDate = addBlogPostViewModel.PublishedDate,
                Author = addBlogPostViewModel.Author,
                Visible = addBlogPostViewModel.Visible,
            };

            // Mapping Tags

            var selectedTags = new List<Tag>();
            foreach (var selectedTagId in addBlogPostViewModel.SelectedTags)
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

            return RedirectToAction("List");
        }

        [HttpGet] 
        public async Task<IActionResult> List()
        {
            var blogPosts = await _blogPostRepository.GetAllPostsAsync();

            return View(blogPosts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var blogPost = await _blogPostRepository.GetPostAsync(id);
            var tagsDomainModel = await _tagRepository.GetAllTagsAsync();

            if(blogPost != null)
            {
                var editBlogPostViewModel = new EditBlogPostViewModel
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    ShortDescription = blogPost.ShortDescription,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    PublishedDate = blogPost.PublishedDate,
                    Author = blogPost.Author,
                    Visible = blogPost.Visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()
                };

                return View(editBlogPostViewModel);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostViewModel editBlogPostViewModel)
        {
            var blogPost = new BlogPost
            {
                Id = editBlogPostViewModel.Id,
                Heading = editBlogPostViewModel.Heading,
                PageTitle = editBlogPostViewModel.PageTitle,
                Content = editBlogPostViewModel.Content,
                ShortDescription = editBlogPostViewModel.ShortDescription,
                FeaturedImageUrl = editBlogPostViewModel.FeaturedImageUrl,
                UrlHandle = editBlogPostViewModel.UrlHandle,
                PublishedDate = editBlogPostViewModel.PublishedDate,
                Author = editBlogPostViewModel.Author,
                Visible = editBlogPostViewModel.Visible
            };

            // Mapping Tags

            var selectedTags = new List<Tag>();
            foreach (var selectedTagId in editBlogPostViewModel.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await _tagRepository.GetTagAsync(selectedTagIdAsGuid);

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }

            blogPost.Tags = selectedTags;

            await _blogPostRepository.UpdatePostAsync(blogPost);

            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostViewModel editBlogPostViewModel)
        {
            await _blogPostRepository.DeletePostAsync(editBlogPostViewModel.Id);

            return RedirectToAction("List");
        }
    
    }
}
