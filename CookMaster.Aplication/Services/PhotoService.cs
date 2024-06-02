using CookMaster.Aplication.DTOs;
using CookMaster.Aplication.Mappings;
using CookMaster.Aplication.Services.Interfaces;
using CookMaster.Persistence.UOW.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;
using CookMaster.Aplication.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CookMaster.Persistance.SqlServer.Model;

namespace CookMaster.Aplication.Services
{
    public class PhotoService : BaseService<Photo>, IPhotoService
    {
        public PhotoService(ILogger<Photo> logger, ISieveProcessor sieveProcessor, IOptions<SieveOptions> sieveOptions, IUnitOfWork unitOfWork)
            : base(logger, sieveProcessor, sieveOptions, unitOfWork)
        {
        }

        public async Task<(bool IsSuccess, Photo? entity, HttpStatusCode StatusCode, string ErrorMessage)> CreateNewPhotoAsync(AddUpdatePhotoDTO dto)
        {
            try
            {
                if (await _unitOfWork.PhotoRepository.PhotoExistsAsync(dto.FileName))
                {
                    return (false, default(Photo), HttpStatusCode.BadRequest, "Photo filename: " + dto.FileName + " already exist in the database");
                }

                if (!await _unitOfWork.RecipeRepository.RecipeExistAsync(dto.IdRecipe))
                {
                    return (false, default(Photo), HttpStatusCode.BadRequest, "Recipe id: " + dto.IdRecipe + " not exist in the database");
                }
                // convert photo to byte
                var Data = PhotoTools.ConvertFromFile2Byte(dto.FilePath);
                var newEntity = dto.MapPhoto(Data);

                var result = await AddAndSaveAsync(newEntity);
                return (true, result.entity, HttpStatusCode.OK, string.Empty);
            }
            catch (Exception ex)
            {
                return LogError(ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Photo? entity, HttpStatusCode StatusCode, string ErrorMessage)> DeatachPhotosFromRecipeAsync(int IdRecipe)
        {
            try
            {
                ICollection<Photo> photos = _unitOfWork.PhotoRepository.GetPhotosByIdRecipe(IdRecipe);
                // add IdRecipe null to photos
                foreach (Photo p in photos)
                {
                    p.IdRecipe = null;
                    var result = await UpdateAndSaveAsync(p, p.Id);
                }
                return (true,default(Photo), HttpStatusCode.OK, string.Empty);
            }
            catch (Exception ex)
            {
                return LogError(ex.Message);
            }

        }

        public async Task<(bool IsSuccess, Photo? entity, HttpStatusCode StatusCode, string ErrorMessage)> UpdatePhotoAsync(AddUpdatePhotoDTO dto, int id)
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
                    return (false, default(Photo), HttpStatusCode.BadRequest, "Recipe id: " + dto.IdRecipe + " not exist in the database");
                }
                // convert photo to byte
                var Data = PhotoTools.ConvertFromFile2Byte(dto.FilePath);

                var domainEntity = dto.MapPhoto(Data);

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
