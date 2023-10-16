using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpiceyLaughs.Model;
using SpiceyLaughs.Services;

namespace SpiceyLaughs.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ICategoryService _service;

        public EditModel(ICategoryService service)
        {
            this._service = service;
        }
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Category = await _service.GetCategoryById(Id);
            if(Category == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                Category.Id = Id;
                await _service.UpdateCategory(Category);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
