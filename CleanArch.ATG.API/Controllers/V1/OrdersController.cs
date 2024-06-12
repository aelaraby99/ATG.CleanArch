using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
namespace CleanArch.ATG.API.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class OrdersController : ControllerBase
    {
    }
}
