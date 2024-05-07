using CookMaster.Persistance.SqlServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Persistence.Repositories.Interfaces
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task<bool> EmailExistsAsync(string email);
        Task<bool> PasswordCorrectAsync(string password, int id);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> IsPasswordEditAllowedAsync(string password, int id);
    }
}
