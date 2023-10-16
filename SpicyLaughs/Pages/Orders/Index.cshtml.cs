using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using SpiceyLaughs.Services;

namespace SpiceyLaughs.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IDishService _service;
        private readonly IShoppingCart _shoppingCartModel;

        public IndexModel(IDishService service, IShoppingCart shoppingCartModel)
        {
            this._service = service;
            this._shoppingCartModel = shoppingCartModel;
        }
        [BindProperty]
        public IShoppingCart ShoppingCart { get; set; }
        [BindProperty]
        public double ShoppingCartTotal { get; set; }
        public void OnGet()
        {
            var items = _shoppingCartModel.GetAllShoppingCartItems();
            _shoppingCartModel.ShoppingCartItems = items;
            ShoppingCart = _shoppingCartModel;
            ShoppingCartTotal = (double)_shoppingCartModel.GetShoppingCartTotal();

        }
    }
}
