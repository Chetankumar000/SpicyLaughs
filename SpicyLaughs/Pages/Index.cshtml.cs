using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpiceyLaughs.Model;
using SpiceyLaughs.Services;

namespace SpicyLaughs.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDishService _service;

        public IndexModel(ILogger<IndexModel> logger, IDishService service)
        {
            _logger = logger;
            this._service = service;
        }
        [BindProperty]
        public IEnumerable<Dish> Dishes { get; set; }
        public async Task OnGet()
        {
            var dishes = await _service.GetAllDishes();
            Dishes = dishes.Where(dish => dish.Available == true).Take(3).ToList();
        }
    }
}