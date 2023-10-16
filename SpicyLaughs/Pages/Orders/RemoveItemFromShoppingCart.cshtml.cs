using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpiceyLaughs.Services;

namespace SpiceyLaughs.Pages.Orders
{
    public class RemoveItemFromShoppingCartModel : PageModel
    {
        private readonly IShoppingCart _shoppingCartModel;
        private readonly IDishService _service;

        public RemoveItemFromShoppingCartModel(IShoppingCart shoppingCartModel, IDishService service)
        {
            this._shoppingCartModel = shoppingCartModel;
            this._service = service;
        }
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var item = await _service.GetDishById(Id);
            if (item != null)
            {
                _shoppingCartModel.RemoveItemFromCart(item);
            }
            return RedirectToPage("Index");
        }
    }
}
