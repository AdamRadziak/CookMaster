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
    public class RecipeService : BaseService<Recipe>, IRecipeService
    {
        public RecipeService(ILogger<Recipe> logger, ISieveProcessor sieveProcessor, IOptions<SieveOptions> sieveOptions, IUnitOfWork unitOfWork) : base(logger, sieveProcessor, sieveOptions, unitOfWork)
        {
        }

        public Task<(bool IsSuccess, User? entity, HttpStatusCode StatusCode, string ErrorMessage)> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
