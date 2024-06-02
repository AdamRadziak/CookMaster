using CookMaster.Persistance.SqlServer.Model;

namespace CookMaster.Persistence.Repositories.Interfaces
{
    public interface IUserMenuRepository : IGenericRepository<UserMenu>
    {
        Task<UserMenu> GetByIdAsync(int id);

        IQueryable<UserMenu> GetAllByUserIdAsync(int count, int IdUser);

        Task<bool> IsMenuExistAsync(int id);

    }
}
