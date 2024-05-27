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
using static Sieve.Extensions.MethodInfoExtended;

namespace CookMaster.Aplication.Services
{
    public class RecipeService : BaseService<Recipe>, IRecipeService
    {
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
                var query = _unitOfWork.RecipeRepository.GetAllAsync(50) ;

                var result = await query.ToPagedListAsync(_sieveProcessor,_sieveOptions,paginationParams, resultEntity => Domain2DTOMapper.MapGetSingleRecipeDTO(resultEntity));

                return (true, result, HttpStatusCode.OK, String.Empty);
            }
            catch (Exception ex)
            {
                var error = LogError(ex.Message);

                return (false, default, error.StatusCode, error.ErrorMessage);
            }
        }

        public async Task<(bool IsSuccess, Recipe? entity, HttpStatusCode StatusCode, string ErrorMessage)> AddRecipe2FavouritesAsync(int IdRecipe, int IdUser)
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
                if (!await _unitOfWork.UserRepository.IsUserExistAsync(IdUser))
                {
                    return (false, default(Recipe), HttpStatusCode.BadRequest, "User Id" + IdUser + "not exist in the database");
                }
                // add selected IdUser to IdUser in Recipe
                existingEntityResult.entity.IdUser = IdUser;


                return await UpdateAndSaveAsync(existingEntityResult.entity, IdRecipe);

            }
            catch (Exception ex)
            {
                var error = LogError(ex.Message);

                return (false, default, error.StatusCode, error.ErrorMessage);
            }
        }
    }
}
