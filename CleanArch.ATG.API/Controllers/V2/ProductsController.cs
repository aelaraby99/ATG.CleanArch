using Asp.Versioning;
using CleanArch.ATG.API.Utilities;
using CleanArch.ATG.Application.Features.ProductFeatures.Commands.AddProductCommands;
using CleanArch.ATG.Application.Features.ProductFeatures.Commands.DeleteProductCommands;
using CleanArch.ATG.Application.Features.ProductFeatures.Commands.UpdateProductCommands;
using CleanArch.ATG.Application.Features.ProductFeatures.Queries;
using CleanArch.ATG.Application.Interfaces;
using CleanArch.ATG.Domain.Entities;
using CleanArch.ATG.Infrastructure.Contexts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System.Data;
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
    //[Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductsController> _logger;
        private readonly ATGDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController( IMediator mediator , ILogger<ProductsController> logger , ATGDbContext context , IUnitOfWork unitOfWork )
        {
            _mediator = mediator;
            _logger = logger;
            _context = context;
            _unitOfWork = unitOfWork;
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
            throw new Exception();
            var products = await _mediator.Send(new GetProductsQuery());
            return Ok(products.ToList());
        }
        [HttpGet("{id:int}")]
        [HasPermission("NoPermission")]
        public async Task<ActionResult<Product>> GetProductById( int id )
        {
            _logger.LogWarning($"GetProductById {id} called");
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            //throw new Exception("This is a test exception");
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
        public async Task<ActionResult<Product>> AddProduct( AddProductCommand product ) 
            => Ok(await _mediator.Send(product));

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
        [HttpGet("BookByAuthor")]
        public IActionResult GetBookByAuthor( string authorName )
        {
            var authorNameParameter = new OracleParameter("Auth_Name" , OracleDbType.NVarchar2)
            {
                Value = authorName
            };

            var titleParameter = new OracleParameter
            {
                ParameterName = "title" ,
                OracleDbType = OracleDbType.NVarchar2 ,
                Direction = ParameterDirection.Output ,
                Size = 255
            };

            var authorNameOutputParameter = new OracleParameter
            {
                ParameterName = "authorName" ,
                OracleDbType = OracleDbType.NVarchar2 ,
                Direction = ParameterDirection.Output ,
                Size = 255
            };

            _context.Database.ExecuteSqlRaw("BEGIN GetBooks(:Auth_Name, :title, :authorName); END;" ,
                                           authorNameParameter , titleParameter , authorNameOutputParameter);

            var book = new BookByAuthor()
            {
                Title = titleParameter.Value.ToString() ,
                AuthorName = authorNameOutputParameter.Value.ToString()
            };

            return Ok(book);
        }
        //[HttpGet("BooksByAuthor")]
        //public IActionResult GetBooksByAuthor( string authorName )
        //{
        //    var authorNameParameter = new OracleParameter("Auth_Name" , OracleDbType.NVarchar2)
        //    {
        //        Value = authorName
        //    };

        //    var booksCursorParameter = new OracleParameter
        //    {
        //        ParameterName = "books_cursor" ,
        //        OracleDbType = OracleDbType.RefCursor ,
        //        Direction = ParameterDirection.Output
        //    };

        //    var books = _context.Set<BookByAuthor>().FromSqlRaw("BEGIN GetAllBooks(:Auth_Name, :books_cursor); END;" ,
        //                                          authorNameParameter , booksCursorParameter).ToList();

        //    return Ok(books);
        //}
        [HttpGet("TempBooks")]
        public async Task<IActionResult> GetTempBooksByAuthorAsync( string authorName )
        {
            var authorNameParameter = new OracleParameter("Auth_Name" , OracleDbType.NVarchar2)
            {
                Value = authorName
            };

            await _context.Database.ExecuteSqlRawAsync("BEGIN GetAllTempBooks(:Auth_Name); END;" , authorNameParameter);

            /*SELECT* FROM "NSAPOC"."TEMP_BOOKS"*/
            var books = _context.Set<BookByAuthor>().FromSqlRaw("select * from temp_books").ToList();

            return Ok(books);
        }
        [AllowAnonymous]
        [HttpPost("CreateBook")]
        public async Task<IActionResult> CreateBook( [FromBody] Book book , string name )
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var createdBook = await _unitOfWork.GenericRepository<Book>().AddAsync(book);

                    var library = new Brand
                    {
                        Name = name
                    };

                    var createdLibrary = await _unitOfWork.GenericRepository<Brand>().AddAsync(library);

                    await _unitOfWork.CompleteAsync();
                    transaction.Commit();
                    return Ok(new { createdBook , createdLibrary });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}