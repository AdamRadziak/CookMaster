using CookMaster.Persistance.SqlServer.Model;

namespace CookMaster.Persistence.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> EmailExistsAsync(string email);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> IsPasswordEditAllowedAsync(string password, int id);

        Task<bool> IsEmailEditAllowedAsync(string email, int id);

        Task<bool> IsUserExistAsync(int id);
    }
}
