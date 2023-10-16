using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpiceyLaughs.Model;
using SpiceyLaughs.Services;

namespace SpiceyLaughs.Pages.Dishes
{
    public class DeleteModel : PageModel
    {
        private readonly IDishService _service;

        public DeleteModel(IDishService service)
        {
            this._service = service;
        }
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public Dish DishDetail { get; set; }
        [BindProperty(SupportsGet = true)]
        public IEnumerable<Category> Categories { get; set; }
        public async Task OnGetAsync()
        {
            DishDetail = await _service.GetDishById(Id);
            Categories = await _service.GetAllCategories();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                Categories = await _service.GetAllCategories();
                return Page();
            }
            //DishDetail.Available = true;
            //DishDetail.Id = Id;
            await _service.DeleteDish(Id);
            return RedirectToPage("index");
        }
    }
}
