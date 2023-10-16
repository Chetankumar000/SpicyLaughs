using SpiceyLaughs.Model;

namespace SpiceyLaughs.Services
{
    public interface IOrderService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string UserId, string EmailId , int paymentType);

        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string role);

        Task<long> GetTotalOrders();

        Task<long> GetLast24HoursTotalOrders();

        double? GetTotalOrdersCollection();

        double? GetLast24HoursTotalOrdersCollection();

    }
}
