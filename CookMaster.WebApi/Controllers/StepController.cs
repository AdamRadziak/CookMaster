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
    [Route("api/Steps")]
    public class StepController : ControllerBase
    {
        private readonly IStepService _service;
        private readonly ILogger<StepController> _logger;

        public StepController(IStepService service, ILogger<StepController> logger)
        {
            _service = service; 
            _logger = logger;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = "GetStepById")]
        public async Task<ActionResult<GetSingleStepDTO>> GetStep(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Read error.", detail: result.ErrorMessage);
            }

            return StatusCode((int)result.StatusCode, result.entity!.MapGetSingleStepDTO());
        }

        [HttpGet("list")]
        [SwaggerOperation(OperationId = "GetAllSteps")]
        public async Task<ActionResult<IPagedList<GetSingleStepDTO>>> GetAllSteps([FromQuery] SieveModel paginationParams)
        {
            var result = await _service.SearchAsync(paginationParams, resultEntity => Domain2DTOMapper.MapGetSingleStepDTO(resultEntity));

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Read error.", detail: result.ErrorMessage);
            }

            return Ok(result.entityList);
        }

        [HttpPost("add")]
        [SwaggerOperation(OperationId = "AddStep")]
        public async Task<ActionResult<GetSingleStepDTO>> AddStep([FromBody] AddUpdateStepDTO dto)
        {
            var result = await _service.CreateNewStepAsync(dto);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Add error.", detail: result.ErrorMessage);
            }

            return CreatedAtAction(nameof(GetStep), new { id = result.entity.Id }, result.entity.MapGetSingleStepDTO());
        }

        [HttpPut("update/{id}")]
        [SwaggerOperation(OperationId = "UpdateStep")]
        public async Task<ActionResult<GetSingleStepDTO>> UpdateStep(int id, [FromBody] AddUpdateStepDTO dto)
        {
            var result = await _service.UpdateStepAsync(dto, id);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Update error.", detail: result.ErrorMessage);
            }

            return StatusCode((int)result.StatusCode, result.entity.MapGetSingleStepDTO());
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(OperationId = "DeleteStep")]
        public async Task<IActionResult> DeleteStep(int id)
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
