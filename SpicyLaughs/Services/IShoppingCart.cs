using Microsoft.EntityFrameworkCore;
using SpiceyLaughs.Model;


namespace SpiceyLaughs.Services
{
    public interface IShoppingCart
    {


        string CartId { get; set; }

        List<ShoppingCartItem> ShoppingCartItems { get; set; }


        Task ClearShoppingCartAsync();

        void AddItemToCart(Dish dish);

        void RemoveItemFromCart(Dish dish);
        List<ShoppingCartItem> GetAllShoppingCartItems();

        double? GetShoppingCartTotal();

    }


}
