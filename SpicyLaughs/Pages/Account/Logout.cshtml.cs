using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpiceyLaughs.Model;
using SpiceyLaughs.Services;

namespace PractiseProject.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly IShoppingCart _shoppingCart;

        public LogoutModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context,IShoppingCart shoppingCart)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._context = context;
            this._shoppingCart = shoppingCart;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            await _signInManager.SignOutAsync();
            await _shoppingCart.ClearShoppingCartAsync();
            return RedirectToPage("/Index");
        }
    }
}
