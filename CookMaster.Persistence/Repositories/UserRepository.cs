using CookMaster.Persistance.SqlServer.Context;
using CookMaster.Persistance.SqlServer.Model;
using CookMaster.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookMaster.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(CookMasterDbContext dbContext) : base(dbContext) { }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await Entities.AnyAsync(e => e.Email == email);
        }

        public async Task<bool> PasswordCorrectAsync(string password, int id)
        {
            return await Entities.AnyAsync(e => e.Password == password && e.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await Entities.FirstOrDefaultAsync(e => e.Email == email);
        }


        public async Task<bool> IsPasswordEditAllowedAsync(string password, int id)
        {
            return await Entities.AnyAsync(e => e.Password != password && e.Id == id);
        }

        public async Task<bool> IsEmailEditAllowedAsync(string email, int id)
        {
            return await Entities.AnyAsync(e => e.Email != email && e.Id == id);
        }
    }
}
