using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpiceyLaughs.Model;
using SpiceyLaughs.Services;

namespace SpiceyLaughs.Pages.Dishes
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly IDishService _service;

        public IndexModel(IDishService service)
        {
            this._service = service;
        }
        [BindProperty(SupportsGet = true)]
        public string searchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public DietaryPrefernce foodChoice { get; set; }
        [BindProperty]
        public IEnumerable<Dish> Dishes { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Cuisine { get; set; }
        [BindProperty(SupportsGet = true)]
        public IEnumerable<Category> Categories { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Sort { get; set; }
        public async Task OnGet()
        {
            Dishes =  await _service.GetAllDishesByFilter(searchString);
            Categories = await _service.GetAllCategories();

            if (foodChoice != DietaryPrefernce.All_Items)
            {
                Dishes = Dishes.Where(n=>n.DietaryPrefernce == foodChoice).ToList();
            }
            if(Cuisine != null)
            {
                Dishes = Dishes.Where(n=>n.Category.Title == Cuisine).ToList();
            }
            if(Sort != null)
            {
                if(Sort == "Ascending")
                {
                    Dishes = Dishes.OrderBy(n=>n.Price).ToList();
                }
                else
                {
                    Dishes = Dishes.OrderByDescending(n=>n.Price).ToList();
                }
            }

        }
        public async Task OnPost()
        {
            
        }
    }
}
