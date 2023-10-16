using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpiceyLaughs.Model;
using SpiceyLaughs.Services;
using System.ComponentModel.DataAnnotations;

namespace SpiceyLaughs.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public LoginModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._context = context;
        }
        [Required(ErrorMessage = "Email is Required")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [BindProperty]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [BindProperty]
        public string? Password { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await _userManager.FindByEmailAsync(Email);
            if (user != null)
            {
                var psdCheck = await _userManager.CheckPasswordAsync(user, Password);
                if (psdCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToPage("/Dishes/Index");
                    }
                }
            }
            TempData["Error"] = "Wrong Credentials, Please Try Again";
            return Page();
        }
    }
}
