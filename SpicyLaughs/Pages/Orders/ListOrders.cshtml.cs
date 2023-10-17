using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpiceyLaughs.Model;
using SpiceyLaughs.Services;
using System.Security.Claims;

namespace SpiceyLaughs.Pages.Orders
{
    public class ListOrdersModel : PageModel
    {
        private readonly IOrderService _orderService;

        public ListOrdersModel(IOrderService orderService)
        {
            this._orderService = orderService;
        }
        [BindProperty]
        public List<Order> Orders { get; set; }
        [BindProperty]
        public long OrderCount { get; set; }
        [BindProperty]
        public double? TotalSales { get; set; }
        [BindProperty]
        public long Last24Orders { get; set; }
        [BindProperty]
        public double? Total24Sales { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Status { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Payment { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            Orders = await _orderService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            OrderCount = await _orderService.GetTotalOrders();
            TotalSales = _orderService.GetTotalOrdersCollection();
            Last24Orders = await _orderService.GetLast24HoursTotalOrders();
            Total24Sales = _orderService.GetLast24HoursTotalOrdersCollection();
            if(Status != null)
            {
                Orders = Orders.Where(n=>n.Status == Status).ToList();
            }
            if(Payment != null)
            {
                Orders = Orders.Where(n=>n.Payment == Payment).ToList();
            }

            return Page();
        }
    }
}
