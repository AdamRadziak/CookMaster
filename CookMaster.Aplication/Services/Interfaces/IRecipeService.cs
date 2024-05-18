using CookMaster.Aplication.DTOs;
using CookMaster.Persistance.SqlServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.Services.Interfaces
{
    public interface IRecipeService : IBaseService<Recipe>
    {
        Task<(bool IsSuccess, User? entity, HttpStatusCode StatusCode, string ErrorMessage)> GetByNameAsync(string name);

    }
}
