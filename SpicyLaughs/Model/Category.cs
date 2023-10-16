using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace SpiceyLaughs.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Title is Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 50")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        [StringLength(400, MinimumLength = 20, ErrorMessage = "Description must be between 20 and 400")]
        public string? Description { get; set; }
        [Display(Name = "Category Image")]
        [Required(ErrorMessage = "Image is Requried")]
        public string? ImageURL { get; set; }

        public List<Dish>? Dishes { get; set; }

    }
}
