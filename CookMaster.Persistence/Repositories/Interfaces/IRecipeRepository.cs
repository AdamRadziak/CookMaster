using CookMaster.Persistance.SqlServer.Model;
using System.Collections;

namespace CookMaster.Persistence.Repositories.Interfaces
{
    public interface IRecipeRepository : IGenericRepository<RecipesDetail>
    {
        Task<bool> RecipeEditAllowedAsync(string name);

        Task<IList?> GetByNameAsync(string name);

    }
}
