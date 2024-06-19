using CookMaster.Aplication.DTOs;
using CookMaster.Aplication.Helpers.PagedList;
using CookMaster.Aplication.Mappings;
using CookMaster.Aplication.Services.Interfaces;
using CookMaster.Aplication.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Reflection;

namespace CookMaster.WebApi.Controllers
{

    [ApiController]
    [Route("api/Users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRecipeService _recipeService;
        private readonly IUserMenuService _userMenuService;

        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, IRecipeService recipeService,
            IUserMenuService userMenuService, ILogger<UserController> logger)
        {
            _userService = userService;
            _recipeService = recipeService;
            _userMenuService = userMenuService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = "GetUserById")]
        public async Task<ActionResult<GetSingleUserDTO>> GetUser(int id)
        {
            var result = await _userService.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Read error.", detail: result.ErrorMessage);
            }

            return StatusCode((int)result.StatusCode, result.entity!.MapGetSingleUserDTO());
        }

        [Authorize]
        [HttpGet("list")]
        [SwaggerOperation(OperationId = "GetUsers")]
        public async Task<ActionResult<IPagedList<GetSingleUserDTO>>> GetUsers([FromQuery] SieveModel paginationParams)
        {
            var result = await _userService.SearchAsync(paginationParams, resultEntity => Domain2DTOMapper.MapGetSingleUserDTO(resultEntity));

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Read error.", detail: result.ErrorMessage);
            }

            return Ok(result.entityList);
        }

        [Authorize]
        [HttpGet("LogInByEmail/{emailHash}/Password/{passwordHash}")]
        [SwaggerOperation(OperationId = "LogInByEmailAndPassword")]
        public async Task<ActionResult<GetSingleUserDTO>> LogInUser(string emailHash, string passwordHash)
        {
            String decodedEmail = Base64EncodeDecode.Base64Decode(emailHash);
            String decodedPassword = Base64EncodeDecode.Base64Decode(passwordHash);
            var result = await _userService.VerifyPasswordByEmail(decodedEmail, decodedPassword);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Read error.", detail: result.ErrorMessage);
            }

            return StatusCode((int)result.StatusCode, result.entity!.MapGetSingleUserDTO());
        }

        [HttpPost("register")]
        [SwaggerOperation(OperationId = "RegisterUser")]
        public async Task<ActionResult<GetSingleUserDTO>> AddUser([FromBody] AddUpdateUserDTO dto)
        {
            var result = await _userService.CreateNewUserAsync(dto);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Add error.", detail: result.ErrorMessage);
            }

            return CreatedAtAction(nameof(GetUser), new { id = result.entity.Id }, result.entity.MapGetSingleUserDTO());
        }

        [Authorize]
        [HttpPut("updatePass/{id}")]
        [SwaggerOperation(OperationId = "UpdateUserPassword")]
        public async Task<ActionResult<GetSingleUserDTO>> UpdateUserPassword(int id, [FromBody] AddUpdateUserDTO dto)
        {
            var result = await _userService.UpdateUserPasswordAsync(dto, id);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Update error.", detail: result.ErrorMessage);
            }

            return StatusCode((int)result.StatusCode, result.entity.MapGetSingleUserDTO());
        }

        [Authorize]
        [HttpPut("updateEmail/{id}")]
        [SwaggerOperation(OperationId = "UpdateUserEmail")]
        public async Task<ActionResult<GetSingleUserDTO>> UpdateUserEmail(int id, [FromBody] AddUpdateUserDTO dto)
        {
            var result = await _userService.UpdateUserEmailAsync(dto, id);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Update error.", detail: result.ErrorMessage);
            }

            return StatusCode((int)result.StatusCode, result.entity.MapGetSingleUserDTO());
        }

        [Authorize]
        [HttpDelete("{id}")]
        [SwaggerOperation(OperationId = "DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            //rdeatach from userMenu
            var reultUserMenu = await _userMenuService.DeatachUserMenusFromUser(id);
            if (!reultUserMenu.IsSuccess)
            {
                return Problem(statusCode: (int)reultUserMenu.StatusCode, title: "Delete error.", detail: reultUserMenu.ErrorMessage);
            }
            // deatach from recipes
            var resultRecipes = await _recipeService.DeatachFavouriteRecipesFromUser(id);
            if (!resultRecipes.IsSuccess)
            {
                return Problem(statusCode: (int)resultRecipes.StatusCode, title: "Delete error.", detail: resultRecipes.ErrorMessage);
            }
            var result = await _userService.DeleteAndSaveAsync(id);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Delete error.", detail: result.ErrorMessage);
            }

            return NoContent();
        }

        [HttpGet("GetUserPassBy/{email}")]
        [SwaggerOperation(OperationId = "GetUserPassByEmail")]

        public async Task<ActionResult<GetSingleUserDTO>> GetUserPasswordByEmail(string email)
        {
            var result = await _userService.GetPasswordByEmail(email);
            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Read error.", detail: result.ErrorMessage);
            }

            return StatusCode((int)result.StatusCode, result.entity!.MapGetSingleUserDTO());
        }
    }
}
