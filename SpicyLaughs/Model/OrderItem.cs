using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SpiceyLaughs.Model
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public int? Amount { get; set; }

        public double? Price { get; set; }

        public int? DishId { get; set; }
        [ForeignKey("DishId")]
        public Dish? Dish { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order? Order { get; set; }
    }
}
