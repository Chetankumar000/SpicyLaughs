using Microsoft.AspNetCore.Identity;
using SpiceyLaughs.Static;
using System.ComponentModel.DataAnnotations;

namespace SpiceyLaughs.Model
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage ="Full Name is Required")]
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }
        [Display(Name = "Profile ImageURL")]
        public string? ImageURL { get; set; }
        [Required(ErrorMessage = "Address is Required")]
        public string? Address { get; set; }
        [Display(Name = "PIN Code")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "PIN code must be 6 digits long")]
        public string? ContactPinCode { get; set; }
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^9\d{9}$", ErrorMessage = "Phone number must start with 9 and be 10 digits long")]
        public string? ContactPhone { get; set; }
        public string Role { get; set; }
    }
}
