using BlogTangle.Web.Data;
using BlogTangle.Web.Models.Domain;
using BlogTangle.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogTangle.Web.Controllers
{
    public class AdminTagsController : Controller
    {

        private readonly ApplicationDbContext _db;
        public AdminTagsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddTagViewModel addTagViewModel)
        {
            // Mapping AddTagViewModel to Tag domain model
            Tag tag = new Tag
            {
                Name = addTagViewModel.Name,
                DisplayName = addTagViewModel.DisplayName
            };

            _db.Tags.Add(tag);
            _db.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult List()
        {
            List<Tag> tags = _db.Tags.ToList();

            return View(tags);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            Tag tag = _db.Tags.FirstOrDefault(x => x.Id == id);

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
        public IActionResult Edit(EditTagViewModel editTagViewModel)
        {
            Tag tag = new Tag
            {
                Id = editTagViewModel.Id,
                Name = editTagViewModel.Name,
                DisplayName = editTagViewModel.DisplayName
            };

            Tag existingTag = _db.Tags.Find(tag.Id);

            if(existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                _db.SaveChanges();

                return RedirectToAction("List", new { id = editTagViewModel.Id });
            }

            return RedirectToAction("Edit", new { id = editTagViewModel.Id });
            
        



            
        }
    }



}
