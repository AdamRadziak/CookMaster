using CookMaster.Persistance.SqlServer.Model;
using System.Collections;

namespace CookMaster.Persistence.Repositories.Interfaces
{
    public interface IRecipeRepository : IGenericRepository<Recipe>
    {
        Task<Recipe> GetByIdAsync(int id);

        IQueryable<Recipe> GetAllAsync(int count);
        Task<bool> RecipeExistAsync(int id);

        IQueryable<Recipe> GetFavouritiesByUser(string email);


    }
}
