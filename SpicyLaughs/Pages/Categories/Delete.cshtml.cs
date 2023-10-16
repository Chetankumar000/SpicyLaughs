using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpiceyLaughs.Model;
using SpiceyLaughs.Services;

namespace SpiceyLaughs.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ICategoryService _service;

        public DeleteModel(ICategoryService service)
        {
            this._service = service;
        }
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public Category Category { get; set; }
        public async Task<IActionResult> OnGet()
        {
            Category =await _service.GetCategoryById(Id);
            if(Category == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            Category = await _service.GetCategoryById(Id);
            if (Category == null)
            {
                return RedirectToPage("/NotFound");
            }
            await _service.DeleteCategory(Id);
            return RedirectToPage("index");
        }
    }
}
