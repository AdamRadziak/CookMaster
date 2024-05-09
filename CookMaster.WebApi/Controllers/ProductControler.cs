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
    [Route("api/Products")]
    public class ProductControler : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductControler> _logger;
        public ProductControler(IProductService service, ILogger<ProductControler> logger)
        {
            _productService = service;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = "GetProductById")]
        public async Task<ActionResult<GetSingleProductDTO>> GetProduct(int id)
        {
            var result = await _productService.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Read error.", detail: result.ErrorMessage);
            }

            return StatusCode((int)result.StatusCode, result.entity!.MapGetSingleProductDTO());
        }

        [HttpGet("list")]
        [SwaggerOperation(OperationId = "GetProducts")]
        public async Task<ActionResult<IPagedList<GetSingleProductDTO>>> GetProducts([FromQuery] SieveModel paginationParams)
        {
            var result = await _productService.SearchAsync(paginationParams, resultEntity => Domain2DTOMapper.MapGetSingleProductDTO(resultEntity));

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Read error.", detail: result.ErrorMessage);
            }

            return Ok(result.entityList);
        }

        [HttpPost("add")]
        [SwaggerOperation(OperationId = "AddProduct")]
        public async Task<ActionResult<GetSingleProductDTO>> AddProduct([FromBody] AddUpdateProductDTO dto)
        {
            var result = await _productService.CreateNewProductAsync(dto);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Add error.", detail: result.ErrorMessage);
            }

            return CreatedAtAction(nameof(GetProduct), new { id = result.entity.Id }, result.entity.MapGetSingleProductDTO());
        }

        [HttpPut("update/{id}")]
        [SwaggerOperation(OperationId = "UpdateProduct")]
        public async Task<ActionResult<GetSingleProductDTO>> UpdateProduct(int id, [FromBody] AddUpdateProductDTO dto)
        {
            var result = await _productService.UpdateProductAsync(dto, id);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Update error.", detail: result.ErrorMessage);
            }

            return StatusCode((int)result.StatusCode, result.entity.MapGetSingleProductDTO());
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(OperationId = "DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteAndSaveAsync(id);

            if (!result.IsSuccess)
            {
                return Problem(statusCode: (int)result.StatusCode, title: "Delete error.", detail: result.ErrorMessage);
            }

            return NoContent();
        }
    }
}
