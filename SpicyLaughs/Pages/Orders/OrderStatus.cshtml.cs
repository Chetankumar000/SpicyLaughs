using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SpiceyLaughs.Services;

namespace SpiceyLaughs.Pages.Orders
{
    public class OrderStatusModel : PageModel
    {
        private readonly AppDbContext _context;

        public OrderStatusModel(AppDbContext context)
        {
            this._context = context;
        }
        [BindProperty(SupportsGet = true)]
        public string? Status { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public void OnGet()
        {
            
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var order = await _context.Orders.FirstOrDefaultAsync(n => n.Id == Id);

                if (Status == "Accept")
                {
                    order.Status = "Accepted";
                }
                else
                {
                    order.Status = "Declined";
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/Orders/ListOrders");
        }
    }
}
