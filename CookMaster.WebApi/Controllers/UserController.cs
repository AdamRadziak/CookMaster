using CookMaster.Aplication.DTOs;
using CookMaster.Aplication.Helpers.PagedList;
using CookMaster.Aplication.Mappings;
using CookMaster.Aplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace CookMaster.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

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

        [HttpPost("add")]
        [SwaggerOperation(OperationId = "AddUser")]
        public async Task<ActionResult<GetSingleUserDTO>> AddCustomer([FromBody] AddUpdateUserDTO dto)
        {
            var result = await _userService.CreateNewUserAsync(dto);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Add error.", detail: result.ErrorMessage);
            }

            return CreatedAtAction(nameof(GetUser), new { id = result.entity.Id }, result.entity.MapGetSingleUserDTO());
        }

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

        [HttpDelete("{id}")]
        [SwaggerOperation(OperationId = "DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteAndSaveAsync(id);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Delete error.", detail: result.ErrorMessage);
            }

            return NoContent();
        }
    }
}
