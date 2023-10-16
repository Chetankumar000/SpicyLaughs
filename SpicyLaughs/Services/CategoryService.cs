using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SpiceyLaughs.Model;

namespace SpiceyLaughs.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            this._context = context;
        }
        public async Task AddCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            _context.SaveChanges();
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x=>x.Id == id);
            EntityEntry entry = _context.Entry(category);
            entry.State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var hi =  await _context.Categories.ToListAsync();
            return hi;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task UpdateCategory(Category category)
        {
            EntityEntry entry = _context.Entry(category);
            entry.State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
