using CookMaster.Aplication.DTOs;
using CookMaster.Aplication.Services.Interfaces;
using CookMaster.Persistance.SqlServer.Model;
using CookMaster.Persistence.UOW.Interfaces;
using CookMaster.Aplication.Mappings;
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
    public class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(ILogger<Product> logger,
                                  ISieveProcessor sieveProcessor,
                                  IOptions<SieveOptions> sieveOptions,
                                  IUnitOfWork unitOfWork)
            : base(logger, sieveProcessor, sieveOptions, unitOfWork) { }
    public async Task<(bool IsSuccess, Product? entity, HttpStatusCode StatusCode, string ErrorMessage)> CreateNewProductAsync(AddUpdateProductDTO dto)
        {
            try
            {
                if (await _unitOfWork.ProductRepository.ProductEditAllowedAsync(dto.Name))
                {
                    return (false, default(Product), HttpStatusCode.BadRequest, "Product name: " + dto.Name + " already exist in the database");
                }

                if (!await _unitOfWork.RecipeRepository.RecipeExistAsync(dto.IdRecipe))
                {
                    return (false, default(Product), HttpStatusCode.BadRequest, "Recipe id: " + dto.IdRecipe + " not exist in the database");
                }

                var newEntity = dto.MapProduct();

                var result = await AddAndSaveAsync(newEntity);
                return (true, result.entity, HttpStatusCode.OK, string.Empty);
            }
            catch (Exception ex)
            {
                return LogError(ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Product? entity, HttpStatusCode StatusCode, string ErrorMessage)> UpdateProductAsync(AddUpdateProductDTO dto, int id)
        {
            try
            {
                var existingEntityResult = await WithoutTracking().GetByIdAsync(id);

                if (!existingEntityResult.IsSuccess)
                {
                    return existingEntityResult;
                }

                if (await _unitOfWork.ProductRepository.ProductEditAllowedAsync(dto.Name))
                {
                    return (false, default(Product), HttpStatusCode.BadRequest, "Product name: " + dto.Name + " already exist in the database");
                }

                if (!await _unitOfWork.RecipeRepository.RecipeExistAsync(dto.IdRecipe))
                {
                    return (false, default(Product), HttpStatusCode.BadRequest, "Recipe id: " + dto.IdRecipe + " not exist in the database");
                }

                var domainEntity = dto.MapProduct();

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
