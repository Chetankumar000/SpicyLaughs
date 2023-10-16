using SpiceyLaughs.Model;

namespace SpiceyLaughs.Services
{
    public interface IDishService
    {
        Task<IEnumerable<Dish>> GetAllDishes();
        Task<IEnumerable<Dish>> GetAllDishesByFilter(string searchString);


        Task<Dish> GetDishById(int id);

        Task AddDish(Dish dish);

        Task DeleteDish(int id);

        Task UpdateDish(Dish dish);

        Task<IEnumerable<Category>> GetAllCategories();

    }
}
