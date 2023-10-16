using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SpiceyLaughs.Model
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string? Email { get; set; }

        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public DateTime DateTime { get; set; }

        public string? Status { get; set; }
        public string? Payment {  get; set; }

        public List<OrderItem>? OrderItems { get; set; }
    }
}
