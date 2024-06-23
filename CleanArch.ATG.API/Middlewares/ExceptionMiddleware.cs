using CleanArch.ATG.API.ErrorHandlers;
using System.Net;
using System.Text.Json;

namespace CleanArch.ATG.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware( RequestDelegate next , ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync( HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var response = new ErrorDetails()
                {
                    StatusCode = httpContext.Response.StatusCode ,
                    Message = $"Internal Server Error from the custom middleware.||{ex.Message}"
                }.ToString();
                _logger.LogError($"Something went wrong: {response}");
                await httpContext.Response.WriteAsync(response);
            }
        }
    }
}
