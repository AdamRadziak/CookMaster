using CookMaster.Aplication.DTOs;
using CookMaster.Aplication.Mappings;
using CookMaster.Aplication.Services.Interfaces;
using CookMaster.Persistance.SqlServer.Model;
using CookMaster.Persistence.UOW;
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
    public class StepService : BaseService<Step>, IStepService
    {
        public StepService(ILogger<Step> logger,
                                  ISieveProcessor sieveProcessor,
                                  IOptions<SieveOptions> sieveOptions,
                                  IUnitOfWork unitOfWork)
            : base(logger, sieveProcessor, sieveOptions, unitOfWork) { }

        public async Task<(bool IsSuccess, Step? entity, HttpStatusCode StatusCode, string ErrorMessage)> CreateNewStepAsync(AddUpdateStepDTO dto)
        {
            try
            {
                if (!await _unitOfWork.RecipeRepository.RecipeExistAsync(dto.IdRecipe))
                {
                    return (false, default(Step), HttpStatusCode.BadRequest, "Recipe id: " + dto.IdRecipe + " not exist in the database");
                }

                var newEntity = dto.MapStep();

                var result = await AddAndSaveAsync(newEntity);
                return (true, result.entity, HttpStatusCode.OK, string.Empty);
            }
            catch (Exception ex)
            {
                return LogError(ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Step? entity, HttpStatusCode StatusCode, string ErrorMessage)> DeatachStepsFromRecipeAsync(int IdRecipe)
        {
            try
            {
                ICollection<Step> steps = _unitOfWork.StepRepository.GetStepsByIdRecipe(IdRecipe);
                // add IdRecipe null to photos
                foreach (Step s in steps)
                {
                    s.IdRecipe = null;
                    var result = await UpdateAndSaveAsync(s, s.Id);
                }
                return (true, default(Step), HttpStatusCode.OK, string.Empty);
            }
            catch (Exception ex)
            {
                return LogError(ex.Message);
            }

        }

    public async Task<(bool IsSuccess, Step? entity, HttpStatusCode StatusCode, string ErrorMessage)> UpdateStepAsync(AddUpdateStepDTO dto, int id)
    {
        try
        {
            var existingEntityResult = await WithoutTracking().GetByIdAsync(id);

            if (!existingEntityResult.IsSuccess)
            {
                return existingEntityResult;
            }

            if (!await _unitOfWork.RecipeRepository.RecipeExistAsync(dto.IdRecipe))
            {
                return (false, default(Step), HttpStatusCode.BadRequest, "Recipe id: " + dto.IdRecipe + " not exist in the database");
            }


            var domainEntity = dto.MapStep();

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


