using Microsoft.AspNetCore.Mvc;
using SpiceyLaughs.Services;

namespace SpiceyLaughs.ViewComponents
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly IShoppingCart _shoppingCart;

        public ShoppingCartSummary(IShoppingCart shoppingCart)
        {
            this._shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetAllShoppingCartItems();
            return View(items.Count);
        }
    }
}
