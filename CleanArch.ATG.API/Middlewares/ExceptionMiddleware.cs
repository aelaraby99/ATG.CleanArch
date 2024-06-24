using CleanArch.ATG.API.ErrorHandlers;
using NLog;
using System.Net;

namespace CleanArch.ATG.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware( RequestDelegate next , ILogger<ExceptionMiddleware> logger )
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync( HttpContext httpContext )
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                _logger.LogError($"Exception Occured: {ex.Message}");
                var response = new ErrorDetails()
                {
                    StatusCode = httpContext.Response.StatusCode ,
                    Message = ex.Message
                }.ToString();
                _logger.LogError($"Something went wrong: {response}");
                await httpContext.Response.WriteAsync(response);
            }
        }
    }
}
