using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpiceyLaughs.Services;
using System.Security.Claims;

namespace SpiceyLaughs.Pages.Orders
{
    public class CompleteModel : PageModel
    {

        private readonly IShoppingCart _shoppingCart;
        private readonly IOrderService _ordersService;

        public CompleteModel(IShoppingCart shoppingCartModel, IOrderService ordersService)
        {
            this._shoppingCart = shoppingCartModel;
            this._ordersService = ordersService;
        }
        [BindProperty(SupportsGet =true)]
        public int PaymentType { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var items = _shoppingCart.GetAllShoppingCartItems();
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string Email = User.FindFirstValue(ClaimTypes.Email);

            if (!items.Any())
            {
                return RedirectToPage("/Orders/Index");
            }
            await _ordersService.StoreOrderAsync(items, UserId, Email,PaymentType);

            await _shoppingCart.ClearShoppingCartAsync();
            return Page();
        }

    }
}
