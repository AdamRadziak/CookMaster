using CookMaster.Aplication.DTOs;
using CookMaster.Persistance.SqlServer.Model;
using System.Net;

namespace CookMaster.Aplication.Services.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        Task<(bool IsSuccess, User? entity, HttpStatusCode StatusCode, string ErrorMessage)> CreateNewUserAsync(AddUpdateUserDTO dto);
        Task<(bool IsSuccess, User? entity, HttpStatusCode StatusCode, string ErrorMessage)> UpdateUserPasswordAsync(AddUpdateUserDTO dto, int id);
        Task<(bool IsSuccess, User? entity, HttpStatusCode StatusCode, string ErrorMessage)> UpdateUserEmailAsync(AddUpdateUserDTO dto, int id);
        Task<(bool IsSuccess, User? entity, HttpStatusCode StatusCode, string ErrorMessage)> VerifyPasswordByEmail(string email, string password);

    }
}
