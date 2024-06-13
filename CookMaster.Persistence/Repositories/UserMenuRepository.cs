using CookMaster.Persistance.SqlServer.Context;
using CookMaster.Persistance.SqlServer.Model;
using CookMaster.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookMaster.Persistence.Repositories
{
    public class UserMenuRepository : GenericRepository<UserMenu>, IUserMenuRepository
    {
        IRecipeRepository recipeRepo;
        public UserMenuRepository(CookMasterDbContext dbContext) : base(dbContext)
        {
            recipeRepo = new RecipeRepository(dbContext);
        }


        public IQueryable<UserMenu> GetAllByUserIdAsync(int c, int IdUser)
        {
            var query = Entities.Include(e => e.Recipes)
                                       .Take(c).Where(e => e.IdUser == IdUser)
                                       .AsQueryable();

            return query;
        }

        public async Task<UserMenu> GetByIdAsync(int id)
        {
            var query = await Entities.Include(e => e.Recipes)
                                       .FirstOrDefaultAsync(e => e.Id == id);

            return query;
        }

        public async Task<bool> IsMenuExistAsync(int id)
        {
            return await Entities.AnyAsync(e => e.Id == id);
        }
    }
}
