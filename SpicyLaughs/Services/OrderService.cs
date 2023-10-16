using Microsoft.EntityFrameworkCore;
using SpiceyLaughs.Model;
using SpiceyLaughs.Static;

namespace SpiceyLaughs.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string role)
        {
            var orders = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n=>n.Dish).Include(n=>n.User).ToListAsync();
            if(role == UserRoles.User)
            {
                orders = orders.Where(o => o.UserId == userId).ToList();
            }
            orders.Reverse();
            return orders;
        }

        public async Task<long> GetLast24HoursTotalOrders()
        {
            var currentTime = DateTime.Now;
            var time24HoursAgo = currentTime.AddHours(-24);
            var orders = await _context.Orders.Where(o=> (o.DateTime >= time24HoursAgo && o.DateTime <= currentTime  )).ToListAsync();
            return orders.Count();
        }

        public double? GetTotalOrdersCollection()
        {
            return _context.OrderItems.Select(o => o.Price * o.Amount).Sum();
        }
        public double? GetLast24HoursTotalOrdersCollection()
        {
            var currentTime = DateTime.Now;
            var time24HoursAgo = currentTime.AddHours(-24);
            var orders = _context.Orders.Where(o => (o.DateTime >= time24HoursAgo && o.DateTime <= currentTime)).Include(n => n.OrderItems).ToList();
            return orders.Select(o => o.OrderItems.Sum(n => n.Price * n.Amount)).Sum();    
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string UserId, string EmailId, int paymentType)
        {
            
            var order = new Order()
            {
                UserId = UserId,
                Email = EmailId,
                DateTime = DateTime.Now,
                Status = "Pending",
                Payment = paymentType==1 ? "COD" : "Online"
            

            
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach(var item in items)
            {
                var orderItem = new OrderItem()
                {
                    DishId = item.Dish.Id,
                    Amount = item.Amount,
                    Price = item.Dish.Price,
                    OrderId = order.Id,
                    
                };
                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<long> GetTotalOrders()
        {
            var orders = _context.Orders.ToList();
            return orders.Count();
        }
    }
}
