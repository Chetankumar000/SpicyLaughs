using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpiceyLaughs.Model;
using SpiceyLaughs.Services;

namespace SpiceyLaughs.Pages.Dishes
{
    [AllowAnonymous]

    public class DetailsModel : PageModel
    {
        private readonly IDishService _service;

        public DetailsModel(IDishService service)
        {
            this._service = service;
        }
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public Dish DishDetail { get; set; }
        public async Task OnGetAsync()
        {
            DishDetail = await _service.GetDishById(Id);
        }
    }
}
