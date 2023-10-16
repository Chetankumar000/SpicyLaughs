using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpiceyLaughs.Model
{
    public class Dish
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 50")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        [StringLength(400, MinimumLength = 20, ErrorMessage = "Description must be between 20 and 400")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is Requried")]
        public int? Price { get; set; }
        [Display(Name = "Dish Image URL")]
        [Required(ErrorMessage = "Image is Requried")]
        public string? ImageURL { get; set; }
        [Required(ErrorMessage = "Calories is Requried")]
        public int? Calories { get; set; }
        [Display(Name = "Prep Time")]
        [Required(ErrorMessage = "Time is Requried")]
        public int? Time {  get; set; }
        [Display(Name = "Spice Level")]
        [Required(ErrorMessage = "Spice Level is Requried")]
        public SpiceLevel? SpiceLevel { get; set; }
        [Display(Name = "Dietary Prefernce")]
        [Required(ErrorMessage = "Dietary Preference is Requried")]
        public DietaryPrefernce? DietaryPrefernce { get; set; }
        public bool Available { get; set; }
        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category is Requried")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
