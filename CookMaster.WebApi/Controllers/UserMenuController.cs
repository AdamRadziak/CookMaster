using CookMaster.Aplication.DTOs;
using CookMaster.Aplication.Helpers.PagedList;
using CookMaster.Aplication.Mappings;
using CookMaster.Aplication.Services;
using CookMaster.Aplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace CookMaster.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/UserMenus")]
    public class UserMenuController : ControllerBase
    {
        private readonly IUserMenuService _service;
        private readonly IRecipeService _recipeService;
        private readonly ILogger<UserMenuController> _logger;

        public UserMenuController(IUserMenuService service, IRecipeService recipeService, ILogger<UserMenuController> logger)
        {
            _service = service;
            _recipeService = recipeService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = "GetUserMenuById")]
        public async Task<ActionResult<GetSingleUserMenuDTO>> GetUserMenu(int id)
        {
            var result = await _service.GetByIdAsync(id);
            var recipes = await _recipeService.GetRecipesFromUserMenuAsync(id);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int?)HttpStatusCode.NotFound, title: "Read error.", detail: "Object not found in database");
            }

            return StatusCode((int)HttpStatusCode.OK, result.entity!.MapGetSingleUserMenuDTO(recipes.recipes));
        }

        [HttpGet("listByUserId/{IdUser}")]
        [SwaggerOperation(OperationId = "GetAllMenusByUserId")]
        public async Task<ActionResult<IPagedList<GetSingleUserMenuDTO>>> GetUserMenusById([FromQuery] SieveModel paginationParams,int IdUser)
        {
            var result = await _service.GetListAsyncForUser<GetSingleUserMenuDTO>(paginationParams, IdUser);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Read error.", detail: result.ErrorMessage);
            }

            return Ok(result.entityList);
        }

        [HttpPost("Generate")]
        [SwaggerOperation(OperationId = "GenerateMenu")]
        public async Task<ActionResult<GetSingleUserMenuDTO>> AddRecipe([FromBody] GenerateUserMenuDTO dto)
        {
            var result = await _service.GenerateUserMenuAsync(dto);
            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Add error.", detail: result.ErrorMessage);
            }

            return CreatedAtAction(nameof(GetUserMenu), new { id = result.entity.Id }, result.entity.MapGetSingleUserMenuDTOGenerated());
        }

        [HttpPut("update/{id}")]
        [SwaggerOperation(OperationId = "UpdateUserMenuById")]
        public async Task<ActionResult<GetSingleUserMenuDTO>> UpdateRecipe(int id, [FromBody] AddUpdateUserMenuDTO dto)
        {
            var result = await _service.UpdateUserMenuAsync(dto, id);
            var recipes = await _recipeService.GetRecipesFromUserMenuAsync(id);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Update error.", detail: result.ErrorMessage);
            }

            return StatusCode((int)result.StatusCode, result.entity.MapGetSingleUserMenuDTO(recipes.recipes));
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(OperationId = "DeleteUserMenu")]
        public async Task<IActionResult> DeleteUserMenu(int id)
        {
            await _recipeService.DeatachRecipesFromUserMenuAsync(id);
            var result = await _service.DeleteUserMenuAsync(id);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Delete error.", detail: result.ErrorMessage);
            }

            return NoContent();
        }


    }
}
