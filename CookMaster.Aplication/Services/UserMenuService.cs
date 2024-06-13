using CookMaster.Aplication.DTOs;
using CookMaster.Aplication.Extensions;
using CookMaster.Aplication.Helpers.PagedList;
using CookMaster.Aplication.Mappings;
using CookMaster.Aplication.Services.Interfaces;
using CookMaster.Persistance.SqlServer.Model;
using CookMaster.Persistence.UOW.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.Services
{
    public class UserMenuService : BaseService<UserMenu>, IUserMenuService
    {
        public UserMenuService(ILogger<UserMenu> logger, ISieveProcessor sieveProcessor, IOptions<SieveOptions> sieveOptions, IUnitOfWork unitOfWork) : base(logger, sieveProcessor, sieveOptions, unitOfWork)
        {
        }

        public async Task<(bool IsSuccess, UserMenu? entity, HttpStatusCode StatusCode, string ErrorMessage)> DeleteUserMenuAsync(int Id)
        {
            // add null to foreign key in receipe
            var userMenu = _unitOfWork.UserMenuRepository.GetByIdAsync(Id);
            userMenu.Result.IdUser = null;
            var resultUpdate = UpdateAndSaveAsync(userMenu.Result, Id);
            // delete and save async recipe
            if (resultUpdate.Result.IsSuccess)
            {
                return await DeleteAndSaveAsync(Id);
            }
            else
            {
                return (false, default(UserMenu), HttpStatusCode.BadRequest, "Delete error");
            }
        }

        public async Task<(bool IsSuccess, UserMenu? entity, HttpStatusCode StatusCode, string ErrorMessage)> GenerateUserMenuAsync(GenerateUserMenuDTO dto)
        {
            try
            {
                var recipes_query =  _unitOfWork.RecipeRepository.GenerateRecipeForUserMenu(dto.DayCount,dto.MealCount,dto.Rate,dto.Popularity,dto.PrepareTime);
                
                if (recipes_query.Equals(null))
                {
                    return (false, default(UserMenu), HttpStatusCode.NotFound, "Not found recipes with selected filter");
                }
                // add get recipes list to dto

                var newEntity = dto.GenerateUserMenuMaping(recipes_query);

                var result = await AddAndSaveAsync(newEntity);
                return (true, result.entity, HttpStatusCode.OK, string.Empty);

            }
            catch (Exception ex)
            {
                return LogError(ex.Message);
            }
        }

        public async Task<(bool IsSuccess, UserMenu? entity, HttpStatusCode StatusCode, string ErrorMessage)> GetByIdAsync(int id)
        {
            try
            {
                var query = await _unitOfWork.UserMenuRepository.GetByIdAsync(id);

                if (query.Equals(null))
                {
                    return (false, default(UserMenu), HttpStatusCode.NotFound, "User menu not exist in database");
                }
                else
                {
                    return (true, query, HttpStatusCode.OK, String.Empty);
                }
            }
            catch (Exception ex)
            {
                return LogError(ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IPagedList<GetSingleUserMenuDTO>? entityList, HttpStatusCode StatusCode, string ErrorMessage)> GetListAsyncForUser<TOut>(SieveModel paginationParams, int IdUser)
        {
            try
            {
                ICollection<Recipe> recipes = new List<Recipe>();
                var query = _unitOfWork.UserMenuRepository.GetAllByUserIdAsync(50,IdUser);
                ICollection<UserMenu> menus = query.ToList();
                // get all recipes by Id menu
                foreach (var menu in menus) {
                    var recipes_query = _unitOfWork.RecipeRepository.GetRecipesByIdMenu(menu.Id);
                    foreach (var recipe in recipes_query)
                    {
                        recipes.Add(recipe);
                    }
                        }

                var result = await query.ToPagedListAsync(_sieveProcessor, _sieveOptions, paginationParams, resultEntity => Domain2DTOMapper.MapGetSingleUserMenuDTO(resultEntity,recipes));

                return (true, result, HttpStatusCode.OK, String.Empty);
            }
            catch (Exception ex)
            {
                var error = LogError(ex.Message);

                return (false, default, error.StatusCode, error.ErrorMessage);
            }
        }


        public async Task<(bool IsSuccess, UserMenu? entity, HttpStatusCode StatusCode, string ErrorMessage)> UpdateUserMenuAsync(AddUpdateUserMenuDTO dto, int id)
        {
            try
            {
                var existingEntityResult = await WithoutTracking().GetByIdAsync(id);
                ICollection<Recipe> recipes = new List<Recipe>();
                
                if (!existingEntityResult.IsSuccess)
                {
                    return existingEntityResult;
                }
                // get all recipes by IdRecipe
                foreach(int Id in dto.IdRecipes)
                {
                    var result = await _unitOfWork.RecipeRepository.GetByIdAsync(Id);
                    recipes.Add(result);
                }

                var domainEntity = dto.MapUserMenu(recipes);

                domainEntity.Id = id;


                return await UpdateAndSaveAsync(domainEntity, id);
            }
            catch (Exception ex)
            {
                return LogError(ex.Message);
            }
        }

    }
}
