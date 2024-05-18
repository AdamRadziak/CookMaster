using CookMaster.Aplication.DTOs;
using CookMaster.Aplication.Helpers.PagedList;
using CookMaster.Aplication.Services;
using CookMaster.Aplication.Services.Interfaces;
using CookMaster.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using Swashbuckle.AspNetCore.Annotations;
using static Sieve.Extensions.MethodInfoExtended;
using System.Net;
using CookMaster.Aplication.Mappings;

namespace CookMaster.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Recipes")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _service;
        private readonly IRecipeRepository _repository;
        private readonly ILogger<RecipeController> _logger;

        public RecipeController(IRecipeService service, ILogger<RecipeController> logger, IRecipeRepository repository)
        {
            _service = service;
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = "GetRecipeByName")]
        public async Task<ActionResult<GetSingleRecipeDTO>> GetRecipe(string name)
        {
            var result = await _repository.GetByNameAsync(name);

            if (result == null)
            {
                return Problem(statusCode: (int?)HttpStatusCode.NotFound, title: "Read error.", detail: "Object not found in database");
            }

            return StatusCode((int)HttpStatusCode.OK, result.MapGetSingleRecipeDTO());
        }

        //[HttpGet("list")]
        //[SwaggerOperation(OperationId = "GetRecipes")]
        //public async Task<ActionResult<IPagedList<GetSingleRecipeDTO>>> GetRecipes([FromQuery] SieveModel paginationParams)
        //{
        //    var result = await _service.SearchAsync(paginationParams, resultEntity => Domain2DTOMapper.MapGetSingleRecipeDTO(resultEntity));

        //    if (!result.IsSuccess)
        //    {
        //        return Problem(statusCode: (int)result.StatusCode, title: "Read error.", detail: result.ErrorMessage);
        //    }

        //    return Ok(result.entityList);
        //}

    }
}
