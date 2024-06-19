using CookMaster.Aplication.DTOs;
using CookMaster.Aplication.Mappings;
using CookMaster.Aplication.Services.Interfaces;
using CookMaster.Aplication.Utils;
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
                string DecodedEmail = Base64EncodeDecode.Base64Decode(dto.EmailHash);
                if (await _unitOfWork.UserRepository.EmailExistsAsync(DecodedEmail))
                {
                    return (false, default(User), HttpStatusCode.BadRequest, "Email: " + DecodedEmail + " already registered.");
                }

                var newEntity = dto.MapUser();

                var result = await AddAndSaveAsync(newEntity);
                return (true, result.entity, HttpStatusCode.OK, string.Empty);
            }
            catch (Exception ex)
            {
                return LogError(ex.Message);
            }
        }

        public async Task<(bool IsSuccess, User? entity, HttpStatusCode StatusCode, string ErrorMessage)> GetPasswordByEmail(string emailHash)
        {
            try
            {
                string DecodedEmail = Base64EncodeDecode.Base64Decode(emailHash);
                var existingEntityResult = await _unitOfWork.UserRepository.GetByEmailAsync(DecodedEmail);
                if (existingEntityResult == null)
                {
                    return (false, default(User), HttpStatusCode.BadRequest, "Username with email: " + DecodedEmail + " not exist in the database");
                }



                return (true, existingEntityResult, HttpStatusCode.OK, String.Empty);

                
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

                string DecodedEmail = Base64EncodeDecode.Base64Decode(dto.EmailHash);

                if (!await _unitOfWork.UserRepository.IsEmailEditAllowedAsync(DecodedEmail, id))
                {
                    return (false, default(User), HttpStatusCode.BadRequest, "Email: " + DecodedEmail + " already registered.");
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
                string DecodedPass = Base64EncodeDecode.Base64Decode(dto.PasswordHash);

                if (!await _unitOfWork.UserRepository.IsPasswordEditAllowedAsync(DecodedPass, id))
                {
                    return (false, default(User), HttpStatusCode.BadRequest, "This password: " + DecodedPass + " is the same with previous");
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
