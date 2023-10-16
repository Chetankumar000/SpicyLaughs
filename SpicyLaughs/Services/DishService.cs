using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SpiceyLaughs.Model;

namespace SpiceyLaughs.Services
{
    public class DishService : IDishService
    {
        private readonly AppDbContext _context;

        public DishService(AppDbContext context)
        {
            this._context = context;
        }

        public async Task AddDish(Dish dish)
        {
            await _context.Dishes.AddAsync(dish);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDish(int id)
        {
            var dish = await _context.Dishes.FirstOrDefaultAsync(d => d.Id == id);
            EntityEntry entry = _context.Entry(dish);
            entry.State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.OrderBy(n=>n.Title).ToListAsync();
        }

        public async Task<IEnumerable<Dish>> GetAllDishes()
        {
            var hi =  await _context.Dishes.Include(n=>n.Category).ToListAsync();
            return hi;
        }

        public async Task<IEnumerable<Dish>> GetAllDishesByFilter(string searchString)
        {
            var allDishes = await _context.Dishes.Include(c => c.Category).ToListAsync();
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                return allDishes.Where(n => n.Title.ToLower().Contains(searchString)).ToList();
            }
            return allDishes;
        }

        public async Task<Dish> GetDishById(int id)
        {
            return await _context.Dishes.Include(n => n.Category).FirstOrDefaultAsync(x=>x.Id ==  id);
        }

        public async Task UpdateDish(Dish dish)
        {
            EntityEntry entry = _context.Entry(dish); 
            entry.State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
