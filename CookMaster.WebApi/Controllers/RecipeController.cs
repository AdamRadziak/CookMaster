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
        private readonly IProductService _productService;
        private readonly IStepService _stepService;
        private readonly IPhotoService _photoService;
        private readonly ILogger<RecipeController> _logger;

        public RecipeController(IRecipeService service, IProductService prodService, IStepService stepService, IPhotoService phototService, ILogger<RecipeController> logger)
        {
            _service = service;
            _productService = prodService;
            _stepService = stepService; 
            _photoService = phototService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = "GetRecipeById")]
        public async Task<ActionResult<GetSingleRecipeDTO>> GetRecipe(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int?)HttpStatusCode.NotFound, title: "Read error.", detail: "Object not found in database");
            }

            return StatusCode((int)HttpStatusCode.OK, result.entity!.MapGetSingleRecipeDTO());
        }

        [HttpGet("list")]
        [SwaggerOperation(OperationId = "GetRecipes")]
        public async Task<ActionResult<IPagedList<GetSingleRecipeDTO>>> GetRecipes([FromQuery] SieveModel paginationParams)
        {
            var result = await _service.GetListAsync<GetSingleRecipeDTO>(paginationParams);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Read error.", detail: result.ErrorMessage);
            }

            return Ok(result.entityList);
        }

        [HttpPost("add")]
        [SwaggerOperation(OperationId = "AddRecipe")]
        public async Task<ActionResult<GetSingleRecipeDTO>> AddRecipe([FromBody] AddUpdateRecipeDTO dto)
        {
            var result = await _service.CreateNewRecipeAsync(dto);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Add error.", detail: result.ErrorMessage);
            }

            return CreatedAtAction(nameof(GetRecipe), new { id = result.entity.Id }, result.entity.MapGetSingleRecipeDTO());
        }

        [HttpPut("update/{id}")]
        [SwaggerOperation(OperationId = "UpdateRecipe")]
        public async Task<ActionResult<GetSingleRecipeDTO>> UpdateRecipe(int id, [FromBody] AddUpdateRecipeDTO dto)
        {
            var result = await _service.UpdateRecipeAsync(dto, id);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Update error.", detail: result.ErrorMessage);
            }

            return StatusCode((int)result.StatusCode, result.entity.MapGetSingleRecipeDTO());
        }

        [HttpPost("AddFavourite/{IdRecipe}/ForUser/{Email}")]
        [SwaggerOperation(OperationId = "AddToFavouritites")]
        public async Task<ActionResult<GetSingleRecipeDTO>> AddRecipe2Favouritites(int IdRecipe,string Email)
        {
            var result = await _service.AddRecipe2FavouritesAsync(IdRecipe, Email);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Update error.", detail: result.ErrorMessage);
            }

            return StatusCode((int)result.StatusCode, result.entity.MapGetSingleRecipeDTO());
        }

        [HttpGet("list/GetFavourities/ForUser/{Email}")]
        [SwaggerOperation(OperationId = "GetFavourities")]
        public async Task<ActionResult<IPagedList<GetSingleRecipeDTO>>> GetFavouritites([FromQuery] SieveModel paginationParams, string Email)
        {
            var result = await _service.GetFavouritesAsync<GetSingleRecipeDTO>(Email,paginationParams);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Read error.", detail: result.ErrorMessage);
            }

            return Ok(result.entityList);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(OperationId = "DeleteRecipe")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            await _productService.DeatachProductsFromRecipeAsync(id);
            await _stepService.DeatachStepsFromRecipeAsync(id);
            await _photoService.DeatachPhotosFromRecipeAsync(id);
            var result = await _service.DeleteRecipe(id);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Delete error.", detail: result.ErrorMessage);
            }

            return NoContent();
        }

        [HttpDelete("DeleteFavourite/{id}/ForUser/{Email}")]
        [SwaggerOperation(OperationId = "DeleteRecipeFromFavourities")]
        public async Task<IActionResult> DeleteRecipeFromFavourite(int id, string Email)
        {

            var result = await _service.DeleteFromFavouriteAsync(id,Email);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Delete error.", detail: result.ErrorMessage);
            }

            return NoContent();
        }

    }
}
