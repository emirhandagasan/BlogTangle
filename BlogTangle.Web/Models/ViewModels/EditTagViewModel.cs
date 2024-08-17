using System.Security.Principal;

namespace BlogTangle.Web.Models.ViewModels
{
    public class EditTagViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
