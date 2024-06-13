using CookMaster.Aplication.DTOs;
using CookMaster.Aplication.Extensions;
using CookMaster.Aplication.Helpers.PagedList;
using CookMaster.Aplication.Mappings;
using CookMaster.Aplication.Services.Interfaces;
using CookMaster.Persistance.SqlServer.Model;
using CookMaster.Persistence.UOW;
using CookMaster.Persistence.UOW.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sieve.Extensions;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Sieve.Extensions.MethodInfoExtended;

namespace CookMaster.Aplication.Services
{
    public class RecipeService : BaseService<Recipe>, IRecipeService
    {
        IProductService _productService;
        IPhotoService _photoService;
        IRecipeService _RecipeService;
        public RecipeService(ILogger<Recipe> logger, ISieveProcessor sieveProcessor, IOptions<SieveOptions> sieveOptions, IUnitOfWork unitOfWork) : base(logger, sieveProcessor, sieveOptions, unitOfWork)
        {
        }

        public async Task<(bool IsSuccess, Recipe? entity, HttpStatusCode StatusCode, string ErrorMessage)> CreateNewRecipeAsync(AddUpdateRecipeDTO dto)
        {
            try
            {

                var newEntity = dto.MapRecipe();


                var result = await AddAndSaveAsync(newEntity);
                return (true, result.entity, HttpStatusCode.OK, string.Empty);
            }
            catch (Exception ex)
            {
                return LogError(ex.Message);
            }
        }


        public async Task<(bool IsSuccess, Recipe? entity, HttpStatusCode StatusCode, string ErrorMessage)> UpdateRecipeAsync(AddUpdateRecipeDTO dto, int id)
        {
            try
            {
                var existingEntityResult = await WithoutTracking().GetByIdAsync(id);

                if (!existingEntityResult.IsSuccess)
                {
                    return existingEntityResult;
                }


                var domainEntity = dto.MapRecipe();

                domainEntity.Id = id;


                return await UpdateAndSaveAsync(domainEntity, id);
            }
            catch (Exception ex)
            {
                return LogError(ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Recipe? entity, HttpStatusCode StatusCode, string ErrorMessage)> GetByIdAsync(int id)
        {
            try
            {
                var query = await _unitOfWork.RecipeRepository.GetByIdAsync(id);

                if (query.Equals(null))
                {
                    return (false, default(Recipe), HttpStatusCode.NotFound, "Recipe not exist in database");
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

        public async Task<(bool IsSuccess, IPagedList<GetSingleRecipeDTO>? entityList, HttpStatusCode StatusCode, string ErrorMessage)> GetListAsync<TOut>(SieveModel paginationParams)
        {
            try
            {
                var query = _unitOfWork.RecipeRepository.GetAllAsync(50);

                var result = await query.ToPagedListAsync(_sieveProcessor, _sieveOptions, paginationParams, resultEntity => Domain2DTOMapper.MapGetSingleRecipeDTO(resultEntity));

                return (true, result, HttpStatusCode.OK, String.Empty);
            }
            catch (Exception ex)
            {
                var error = LogError(ex.Message);

                return (false, default, error.StatusCode, error.ErrorMessage);
            }
        }

        public async Task<(bool IsSuccess, Recipe? entity, HttpStatusCode StatusCode, string ErrorMessage)> AddRecipe2FavouritesAsync(int IdRecipe, string UserEmail)
        {
            try
            {
                // first find recipe by id
                var existingEntityResult = await WithoutTracking().GetByIdAsync(IdRecipe);
                if (!existingEntityResult.IsSuccess)
                {
                    return existingEntityResult;
                }
                // if this user not exist
                if (!await _unitOfWork.UserRepository.EmailExistsAsync(UserEmail))
                {
                    return (false, default(Recipe), HttpStatusCode.BadRequest, "User Email" + UserEmail + "not exist in the database");
                }
                // add selected IdUser to IdUser in Recipe
                var userEntity = _unitOfWork.UserRepository.GetByEmailAsync(UserEmail);
                existingEntityResult.entity.IdUser = userEntity.Id;


                return await UpdateAndSaveAsync(existingEntityResult.entity, IdRecipe);

            }
            catch (Exception ex)
            {
                var error = LogError(ex.Message);

                return (false, default, error.StatusCode, error.ErrorMessage);
            }
        }

        public async Task<(bool IsSuccess, IPagedList<GetSingleRecipeDTO>? entityList, HttpStatusCode StatusCode, string ErrorMessage)> GetFavouritesAsync<TOut>(string UserEmail, SieveModel paginationParams)
        {
            try
            {
                // if this user not exist
                if (!await _unitOfWork.UserRepository.EmailExistsAsync(UserEmail))
                {
                    return (false, default(IPagedList<GetSingleRecipeDTO>), HttpStatusCode.BadRequest, "User Email" + UserEmail + "not exist in the database");
                }
                var query = _unitOfWork.RecipeRepository.GetFavouritiesByUser(UserEmail);
                var result = await query.ToPagedListAsync(_sieveProcessor, _sieveOptions, paginationParams, resultEntity => Domain2DTOMapper.MapGetSingleRecipeDTO(resultEntity));
                // if query return null
                if (query.Equals(null))
                {
                    return (false, default(IPagedList<GetSingleRecipeDTO>), HttpStatusCode.NotFound, "User with email " + UserEmail + " do not have favourite recipes");
                }
                else
                {
                    return (true, result, HttpStatusCode.OK, String.Empty);
                }
            }
            catch (Exception ex)
            {
                var error = LogError(ex.Message);

                return (false, default, error.StatusCode, error.ErrorMessage);
            }


        }

        public async  Task<(bool IsSuccess, Recipe? entity, HttpStatusCode StatusCode, string ErrorMessage)> DeleteRecipe(int Id)
        {
            
            // add null to foreign key in receipe
            var recipe = _unitOfWork.RecipeRepository.GetByIdAsync(Id);
            recipe.Result.IdMenu = null;
            recipe.Result.IdUser = null;
            var resultUpdate = UpdateAndSaveAsync(recipe.Result, Id);
            // delete and save async recipe
            if (resultUpdate.Result.IsSuccess)
            {
                return await DeleteAndSaveAsync(Id);
            }
            else
            {
                return (false, default(Recipe), HttpStatusCode.BadRequest, "Delete error");
            }


                
        }

        public async Task<(bool IsSuccess, Recipe? entity, HttpStatusCode StatusCode, string ErrorMessage)> DeleteFromFavouriteAsync(int Id, string Useremail)
        {
            try
            {
                // first find recipe by id
                var existingEntityResult = await WithoutTracking().GetByIdAsync(Id);
                if (!existingEntityResult.IsSuccess)
                {
                    return existingEntityResult;
                }
                // if this user not exist
                if (!await _unitOfWork.UserRepository.EmailExistsAsync(Useremail))
                {
                    return (false, default(Recipe), HttpStatusCode.BadRequest, "User Email" + Useremail + "not exist in the database");
                }
                var query = await _unitOfWork.RecipeRepository.GetFavouritiesByUser(Useremail).ToListAsync();
                // get favourite from user by id
                var existingEntity = query.FirstOrDefault(query => query.Id == Id);
                existingEntityResult.entity.IdUser = null;
                existingEntityResult.entity.IdUserNavigation = null;


                return await UpdateAndSaveAsync(existingEntityResult.entity, Id);

            }
            catch (Exception ex)
            {
                var error = LogError(ex.Message);

                return (false, default, error.StatusCode, error.ErrorMessage);
            }
        }

        public async Task<(bool IsSuccess, Recipe? entity, HttpStatusCode StatusCode, string ErrorMessage)> DeatachRecipesFromUserMenuAsync(int IdMenu)
        {
            try
            {
                ICollection<Recipe> Recipes = _unitOfWork.RecipeRepository.GetRecipesByIdMenu(IdMenu);
                // add IdRecipe null to photos
                foreach (Recipe r in Recipes)
                {
                    r.IdMenu = null;
                    r.IdMenuNavigation = null;
                    var result = await UpdateAndSaveAsync(r, r.Id);
                }
                return (true, default(Recipe), HttpStatusCode.OK, string.Empty);
            }
            catch (Exception ex)
            {
                return LogError(ex.Message);
            }
        }

        public async Task<(bool IsSuccess, ICollection<Recipe> recipes, HttpStatusCode StatusCode, string ErrorMessage)> GetRecipesFromUserMenuAsync(int IdMenu)
        {
            try
            {
                var query =  _unitOfWork.RecipeRepository.GetRecipesByIdMenu(IdMenu);

                if (query.Equals(null))
                {
                    return (false, default(ICollection<Recipe>), HttpStatusCode.NotFound, "Recipe for IdMenu " + IdMenu + " not exist in database");
                }
                else
                {
                    return (true, query, HttpStatusCode.OK, String.Empty);
                }
            }
            catch (Exception ex)
            {
               return (false, default(ICollection<Recipe>), HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
