using CookMaster.Aplication.DTOs;
using CookMaster.Aplication.Helpers.PagedList;
using CookMaster.Persistance.SqlServer.Model;
using Sieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Sieve.Extensions.MethodInfoExtended;

namespace CookMaster.Aplication.Services.Interfaces
{
    public interface IRecipeService : IBaseService<Recipe>
    {
        Task<(bool IsSuccess, Recipe? entity, HttpStatusCode StatusCode, string ErrorMessage)> GetByIdAsync(int id);
        Task<(bool IsSuccess, IPagedList<GetSingleRecipeDTO>? entityList, HttpStatusCode StatusCode, string ErrorMessage)> GetListAsync<TOut>(SieveModel paginationParams);

        Task<(bool IsSuccess, Recipe? entity, HttpStatusCode StatusCode, string ErrorMessage)> CreateNewRecipeAsync(AddUpdateRecipeDTO dto);

        Task<(bool IsSuccess, Recipe? entity, HttpStatusCode StatusCode, string ErrorMessage)> UpdateRecipeAsync(AddUpdateRecipeDTO dto, int id);

        Task<(bool IsSuccess, Recipe? entity, HttpStatusCode StatusCode, string ErrorMessage)> AddRecipe2FavouritesAsync(int IdRecipe, int IdUser);

    }
}
