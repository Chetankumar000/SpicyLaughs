using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using SpiceyLaughs.Model;
using SpiceyLaughs.Services;

namespace PractiseProject.Pages.Orders
{
    public class UsersModel : PageModel
    {
        private readonly AppDbContext _context;

        public UsersModel(AppDbContext context)
        {
            this._context = context;
        }
        [BindProperty]
        public List<ApplicationUser> Users { get; set; }
        public IActionResult OnGet()
        {
            Users =  _context.Users.ToList();
            return Page();
        }
    }
}
