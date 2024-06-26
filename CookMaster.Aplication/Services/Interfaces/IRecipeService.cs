﻿using CookMaster.Aplication.DTOs;
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

        Task<(bool IsSuccess, Recipe? entity, HttpStatusCode StatusCode, string ErrorMessage)> AddRecipe2FavouritesAsync(int IdRecipe, int idUser);        
        Task<(bool IsSuccess, IPagedList<GetSingleRecipeDTO>? entityList, HttpStatusCode StatusCode, string ErrorMessage)> GetFavouritesAsync<TOut>(int idUser, SieveModel paginationParams);

        Task<(bool IsSuccess, Recipe? entity, HttpStatusCode StatusCode, string ErrorMessage)> DeleteRecipe(int Id);

        Task<(bool IsSuccess, Recipe? entity, HttpStatusCode StatusCode, string ErrorMessage)> DeleteFromFavouriteAsync(int Id, int idUser);
        Task<(bool IsSuccess, Recipe? entity, HttpStatusCode StatusCode, string ErrorMessage)> DeatachRecipesFromUserMenuAsync(int IdMenu);
        Task<(bool IsSuccess, Recipe? entity, HttpStatusCode StatusCode, string ErrorMessage)> DeatachFavouriteRecipesFromUser(int IdUser);

        Task<(bool IsSuccess, ICollection<Recipe> recipes, HttpStatusCode StatusCode, string ErrorMessage)> GetRecipesFromUserMenuAsync(int IdMenu);

    }
}
