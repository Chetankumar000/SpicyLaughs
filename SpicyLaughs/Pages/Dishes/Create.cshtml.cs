using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpiceyLaughs.Model;
using SpiceyLaughs.Services;

namespace SpiceyLaughs.Pages.Dishes
{
    public class CreateModel : PageModel
    {
        private readonly IDishService _service;

        public CreateModel(IDishService service)
        {
            this._service = service;
        }
        [BindProperty(SupportsGet = true)]
        public  Dish DishDetail { get; set; }
        [BindProperty(SupportsGet = true)]
        public IEnumerable<Category> Categories { get; set; }
        public async Task OnGetAsync()
        {
            Categories = await _service.GetAllCategories();
        }

        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid)
            {
                Categories = await _service.GetAllCategories();
                return Page();
            }
            //DishDetail.Available = true;
            await _service.AddDish(DishDetail);
            return RedirectToPage("index");
        }
    }
}
