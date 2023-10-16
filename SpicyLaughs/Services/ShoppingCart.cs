using Microsoft.EntityFrameworkCore;
using SpiceyLaughs.Model;

namespace SpiceyLaughs.Services
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly AppDbContext _context;

        public ShoppingCart(AppDbContext context)
        {
            this._context = context;
        }
        public string? CartId { get; set; }
        public List<ShoppingCartItem>? ShoppingCartItems { get; set; }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { CartId = cartId };
        }

        public void AddItemToCart(Dish dish)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Dish.Id == dish.Id && n.ShoppingCartId == CartId);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = CartId,
                    Amount = 1,
                    Dish = dish
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Dish dish)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Dish.Id == dish.Id && n.ShoppingCartId == CartId);
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
        }

        public List<ShoppingCartItem> GetAllShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == CartId).Include(n => n.Dish).ToList());
        }

        public double? GetShoppingCartTotal()
        {
            return _context.ShoppingCartItems.Where(n => n.ShoppingCartId == CartId).Select(n => n.Dish.Price * n.Amount).Sum();
        }

        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItems.Where(n=>n.ShoppingCartId == CartId).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
            ShoppingCartItems = new List<ShoppingCartItem>();
        }
    }
}
