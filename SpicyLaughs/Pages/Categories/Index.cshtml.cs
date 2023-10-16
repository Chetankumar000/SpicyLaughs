using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpiceyLaughs.Model;
using SpiceyLaughs.Services;

namespace SpiceyLaughs.Pages.Categories
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _service;

        public IndexModel(ICategoryService service)
        {
            this._service = service;
        }
        [BindProperty]
        public IEnumerable<Category> Categories { get; set; }
        public async Task OnGet()
        {
            Categories = await _service.GetAllCategories();
        }
    }
}
