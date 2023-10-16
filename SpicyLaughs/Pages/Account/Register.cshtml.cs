using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpiceyLaughs.Model;
using SpiceyLaughs.Services;
using SpiceyLaughs.Static;
using System.ComponentModel.DataAnnotations;

namespace SpiceyLaughs.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._context = context;
        }
        [Required(ErrorMessage = "Full Name is Required")]
        [Display(Name = "Full Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Minimum 3 char Requried")]
        [BindProperty]
        public string? FullName { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [BindProperty]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [BindProperty]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password do not match")]
        [BindProperty]
        public string? ConfirmPassword { get; set; }
        [Display(Name = "Profile ImageURL")]
        [BindProperty]
        public string? ImageURL { get; set; }
        [Required(ErrorMessage = "Address is Required")]
        [BindProperty]
        public string? Address { get; set; }
        [Display(Name = "PIN Code")]
        [Required(ErrorMessage = "Pin Code Is Required")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "PIN code must be 6 digits long")]
        [BindProperty]
        public string? ContactPinCode { get; set; }
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^9\d{9}$", ErrorMessage = "Phone number must start with 9 and be 10 digits long")]
        [Required(ErrorMessage = "Phone Number Is Required")]
        [BindProperty]
        public string? ContactPhone { get; set; }
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
                TempData["Error"] = "This Email is already in use";
                return Page();
            }
            var newUser = new ApplicationUser
            {

                Email = Email,
                FullName = FullName,
                UserName = Email,
                ContactPhone = ContactPhone,
                Address = Address,
                ImageURL = ImageURL ?? "https://static.vecteezy.com/system/resources/previews/002/318/271/non_2x/user-profile-icon-free-vector.jpg",
                ContactPinCode = ContactPinCode,
                Role = UserRoles.User,
            };
            var response = await _userManager.CreateAsync(newUser, Password);
            if (response.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            return RedirectToPage("RegisterCompleted");
        }
    }
}
