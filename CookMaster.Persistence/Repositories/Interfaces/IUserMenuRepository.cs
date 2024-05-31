using CookMaster.Persistance.SqlServer.Model;

namespace CookMaster.Persistence.Repositories.Interfaces
{
    public interface IUserMenuRepository : IGenericRepository<UserMenu>
    {
        Task<UserMenu> GetByIdAsync(int id);

        IQueryable<UserMenu> GetAllByUserEmailAsync(int count, string useremail);

        Task<bool> IsMenuExistAsync(int id);
    }
}
