using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpiceyLaughs.Model;
using SpiceyLaughs.Services;
using SpiceyLaughs.Static;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;


namespace SpiceyLaughs.Pages.User
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly AppDbContext context;

        public ProfileModel(UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
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
        public string Email { get; set; }
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
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public ApplicationUser CurrUser { get; set; }
        [BindProperty]
        public string?  Role { get; set; }
        public async Task OnGet()
        {
            if(Id != null)
            {
               CurrUser = await userManager.FindByIdAsync(Id);
            }
            else
            {
                 CurrUser = await userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            }
            Id = CurrUser.Id;
            FullName = CurrUser.FullName;
            Role = CurrUser.Role;
            ImageURL = CurrUser.ImageURL;
            Address = CurrUser.Address;
            ContactPhone = CurrUser.ContactPhone;
            ContactPinCode = CurrUser.ContactPinCode;
            Email = CurrUser.Email;
        }
        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(Id);
                user.FullName = FullName;
                user.ContactPhone = ContactPhone;
                user.Address = Address;
                user.ContactPinCode = ContactPinCode;
                user.ImageURL = ImageURL;
                user.Role = Role;
                var result = await userManager.UpdateAsync(user);
                if(result.Succeeded)
                {
                    return RedirectToPage("/index");

                }
            }
            return Page();
        }
    }
}
