using CookMaster.Persistance.SqlServer.Context;
using CookMaster.Persistance.SqlServer.Model;
using CookMaster.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookMaster.Persistence.Repositories
{
    public class UserMenuRepository : GenericRepository<UserMenu>, IUserMenuRepository
    {
        public UserMenuRepository(CookMasterDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<UserMenu> GetAllByUserEmailAsync(int c, string useremail)
        {
            var query = Entities.Include(e => e.Recipes)
                                       .Include(e => e.IdUserNavigation.Email)
                                       .Take(c).Where(e => e.IdUserNavigation.Email == useremail)
                                       .AsQueryable();

            return query;
        }

        public async Task<UserMenu> GetByIdAsync(int id)
        {
            var query = await Entities.Include(e => e.Recipes)
                                        .Include(e => e.IdUserNavigation.Email)
                                       .FirstOrDefaultAsync(e => e.Id == id);

            return query;
        }

        public async Task<bool> IsMenuExistAsync(int id)
        {
            return await Entities.AnyAsync(e => e.Id == id);
        }
    }
}
