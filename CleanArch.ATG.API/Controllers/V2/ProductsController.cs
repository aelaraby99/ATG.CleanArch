using Asp.Versioning;
using CleanArch.ATG.Application.Features.ProductFeatures.Commands;
using CleanArch.ATG.Application.Features.ProductFeatures.Queries;
using CleanArch.ATG.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Text.Json;

namespace CleanArch.ATG.API.Controllers.V2
{
    /// <summary>
    /// As developers, we often add new features to our apps and modify current APIs as well. Versioning enables us to safely add new functionality without breaking changes. But not all changes to APIs are breaking changes.
    /// Generally, additive changes are not breaking changes: 
    /// 1- Adding new Endpoints 
    /// 2- New( optional) query string parameters 
    /// 3- Adding new properties to DTOs
    /// https://code-maze.com/aspnetcore-api-versioning/
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController( IMediator mediator , ILogger<ProductsController> logger )
        {
            _mediator = mediator;
            _logger = logger;
        }
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var fruites = Data.Fruits.Where(f => f.StartsWith("B"));
        //    return Ok(fruites);
        //}
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            return Ok(products.ToList());
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProductById( int id )
        {
                _logger.LogWarning($"GetProductById {id} called");
                var product = await _mediator.Send(new GetProductByIdQuery(id));
                if (product != null)
                {
                    _logger.LogInformation(JsonSerializer.Serialize(product));
                    return Ok(product);
                }
                else
                {
                    _logger.LogWarning(id.ToString() + " not found");
                    return NotFound();
                }
        }
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct( [FromBody] Product product )
        {
            var productToReturn = await _mediator.Send(new AddProductCommand(product));
            //await _mediator.Publish(new ProductAddedNotification(productToReturn));
            //return Ok(productToReturn);
            return CreatedAtAction(nameof(GetProductById) , new { id = productToReturn.Id } , productToReturn);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct( int id )
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return NoContent();
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct( int id , Product product )
        {
            if (product.Id != id)
                return BadRequest();
            await _mediator.Send(new UpdateProductCommand(product));
            return Ok();
        }
    }
}