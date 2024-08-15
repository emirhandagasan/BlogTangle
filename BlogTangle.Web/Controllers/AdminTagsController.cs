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

            return View("Add");
        }
    }



}
