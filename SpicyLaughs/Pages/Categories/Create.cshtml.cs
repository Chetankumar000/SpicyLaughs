using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpiceyLaughs.Model;
using SpiceyLaughs.Services;
using System.Reflection.Metadata.Ecma335;

namespace SpiceyLaughs.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService _service;

        public CreateModel(ICategoryService service)
        {
            this._service = service;
        }
        [BindProperty]
        public Category Category { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                await _service.AddCategory(Category);
                return RedirectToPage("index");
            }
            return Page();
        }
    }
}
