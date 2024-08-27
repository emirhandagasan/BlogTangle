using BlogTangle.Web.Data;
using BlogTangle.Web.Interfaces;
using BlogTangle.Web.Models.Domain;
using BlogTangle.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogTangle.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository _tagRepository;


        public AdminTagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddTagViewModel addTagViewModel)
        {
            Tag tag = new Tag
            {
                Name = addTagViewModel.Name,
                DisplayName = addTagViewModel.DisplayName
            };

            await _tagRepository.AddTagAsync(tag);

            return RedirectToAction("List");
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var tags = await _tagRepository.GetAllTagsAsync();

            return View(tags);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await _tagRepository.GetTagAsync(id);

            if(tag != null)
            {
                EditTagViewModel editTagViewModel = new EditTagViewModel
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagViewModel);
            }

            return View(null);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditTagViewModel editTagViewModel)
        {
            Tag tag = new Tag
            {
                Id = editTagViewModel.Id,
                Name = editTagViewModel.Name,
                DisplayName = editTagViewModel.DisplayName
            };

            await _tagRepository.UpdateTagAsync(tag);

            return RedirectToAction("List");   
        }


        [HttpPost]
        public async Task<IActionResult> Delete(EditTagViewModel editTagViewModel)
        {
            await _tagRepository.DeleteTagAsync(editTagViewModel.Id);

            return RedirectToAction("List");
        }
    }

}
