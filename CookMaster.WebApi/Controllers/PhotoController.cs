using CookMaster.Aplication.DTOs;
using CookMaster.Aplication.Helpers.PagedList;
using CookMaster.Aplication.Mappings;
using CookMaster.Aplication.Services;
using CookMaster.Aplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace CookMaster.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Photos")]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _service;
        private readonly ILogger<PhotoController> _logger;

        public PhotoController(IPhotoService service, ILogger<PhotoController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = "GetPhotoById")]
        public async Task<ActionResult<GetSinglePhotoDTO>> GetPhoto(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Read error.", detail: result.ErrorMessage);
            }

            return StatusCode((int)result.StatusCode, result.entity!.MapGetSinglePhotoDTO());
        }

        [HttpGet("list")]
        [SwaggerOperation(OperationId = "GetPhotos")]
        public async Task<ActionResult<IPagedList<GetSinglePhotoDTO>>> GetPhotos([FromQuery] SieveModel paginationParams)
        {
            var result = await _service.SearchAsync(paginationParams, resultEntity => Domain2DTOMapper.MapGetSinglePhotoDTO(resultEntity));

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Read error.", detail: result.ErrorMessage);
            }

            return Ok(result.entityList);
        }

        [HttpPost("add")]
        [SwaggerOperation(OperationId = "AddPhoto")]
        public async Task<ActionResult<GetSinglePhotoDTO>> AddPhoto([FromBody] AddUpdatePhotoDTO dto)
        {
            var result = await _service.CreateNewPhotoAsync(dto);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Add error.", detail: result.ErrorMessage);
            }

            return CreatedAtAction(nameof(GetPhoto), new { id = result.entity.Id }, result.entity.MapGetSinglePhotoDTO());
        }

        [HttpPut("update/{id}")]
        [SwaggerOperation(OperationId = "UpdatePhoto")]
        public async Task<ActionResult<GetSinglePhotoDTO>> UpdatePhoto(int id, [FromBody] AddUpdatePhotoDTO dto)
        {
            var result = await _service.UpdatePhotoAsync(dto, id);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Update error.", detail: result.ErrorMessage);
            }

            return StatusCode((int)result.StatusCode, result.entity.MapGetSinglePhotoDTO());
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(OperationId = "DeletePhoto")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            var result = await _service.DeleteAndSaveAsync(id);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Delete error.", detail: result.ErrorMessage);
            }

            return NoContent();
        }
    }
}
