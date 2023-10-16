using SpiceyLaughs.Model;

namespace SpiceyLaughs.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategories();

        Task<Category> GetCategoryById(int id);

        Task AddCategory(Category category);

        Task DeleteCategory(int id);

        Task UpdateCategory(Category category);
    }
}
