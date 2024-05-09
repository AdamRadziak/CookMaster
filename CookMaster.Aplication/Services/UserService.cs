using CookMaster.Aplication.DTOs;
using CookMaster.Aplication.Mappings;
using CookMaster.Aplication.Services.Interfaces;
using CookMaster.Persistance.SqlServer.Model;
using CookMaster.Persistence.UOW.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;
using System.Net;

namespace CookMaster.Aplication.Services
{
    public class UserService : BaseService<User>, IUserService
    {

        public UserService(ILogger<User> logger,
                                  ISieveProcessor sieveProcessor,
                                  IOptions<SieveOptions> sieveOptions,
                                  IUnitOfWork unitOfWork)
            : base(logger, sieveProcessor, sieveOptions, unitOfWork) { }
        public async Task<(bool IsSuccess, User? entity, HttpStatusCode StatusCode, string ErrorMessage)> CreateNewUserAsync(AddUpdateUserDTO dto)
        {
            try
            {
                if (await _unitOfWork.UserRepository.EmailExistsAsync(dto.Email))
                {
                    return (false, default(User), HttpStatusCode.BadRequest, "Email: " + dto.Email + " already registered.");
                }

                var newEntity = dto.MapUser();
                //newEntity.CreatedAt = DateTime.UtcNow;
                //newEntity.UpdatedAt = newEntity.CreatedAt;

                var result = await AddAndSaveAsync(newEntity);
                return (true, result.entity, HttpStatusCode.OK, string.Empty);
            }
            catch (Exception ex)
            {
                return LogError(ex.Message);
            }
        }


        public async Task<(bool IsSuccess, User? entity, HttpStatusCode StatusCode, string ErrorMessage)> UpdateUserEmailAsync(AddUpdateUserDTO dto, int id)
        {
            try
            {
                var existingEntityResult = await WithoutTracking().GetByIdAsync(id);

                if (!existingEntityResult.IsSuccess)
                {
                    return existingEntityResult;
                }

                if (!await _unitOfWork.UserRepository.IsEmailEditAllowedAsync(dto.Email, id))
                {
                    return (false, default(User), HttpStatusCode.BadRequest, "Email: " + dto.Email + " already registered.");
                }

                var domainEntity = dto.MapUser();

                domainEntity.Id = id;


                return await UpdateAndSaveAsync(domainEntity, id);
            }
            catch (Exception ex)
            {
                return LogError(ex.Message);
            }
        }

        public async Task<(bool IsSuccess, User? entity, HttpStatusCode StatusCode, string ErrorMessage)> UpdateUserPasswordAsync(AddUpdateUserDTO dto, int id)
        {
            try
            {
                var existingEntityResult = await WithoutTracking().GetByIdAsync(id);

                if (!existingEntityResult.IsSuccess)
                {
                    return existingEntityResult;
                }

                if (!await _unitOfWork.UserRepository.IsPasswordEditAllowedAsync(dto.Password, id))
                {
                    return (false, default(User), HttpStatusCode.BadRequest, "This password: " + dto.Password + " is the same with previous");
                }
           
                    var domainEntity = dto.MapUser();

                    domainEntity.Id = id;

                    return await UpdateAndSaveAsync(domainEntity, id);
             
            }
            catch (Exception ex)
            {
                return LogError(ex.Message);
            }
        }

        public async Task<(bool IsSuccess, User? entity, HttpStatusCode StatusCode, string ErrorMessage)> VerifyPasswordByEmail(string email, string password)
        {
            try
            {
                var existingEntity = await _unitOfWork.UserRepository.GetByEmailAsync(email);

                if (existingEntity == null || existingEntity.Password != password)
                {
                    return (false, default(User), HttpStatusCode.Unauthorized, "Invalid email or password.");
                }

                return (true, existingEntity, HttpStatusCode.OK, string.Empty);
            }
            catch (Exception ex)
            {
                return LogError(ex.Message);
            }
        }
    }
}
