using CookMaster.Aplication.DTOs;
using CookMaster.Persistance.SqlServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.Services.Interfaces
{
    public interface IProductService : IBaseService<Product>
    {
        Task<(bool IsSuccess, Product? entity, HttpStatusCode StatusCode, string ErrorMessage)> CreateNewProductAsync(AddUpdateProductDTO dto);

        Task<(bool IsSuccess, Product? entity, HttpStatusCode StatusCode, string ErrorMessage)> UpdateProductAsync(AddUpdateProductDTO dto, int id);

        Task<(bool IsSuccess, Product? entity, HttpStatusCode StatusCode, string ErrorMessage)> DeatachProductsFromRecipeAsync(int IdRecipe);


    }
}
