using CleanArch.ATG.API.ErrorHandlers;
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
            catch (FluentValidation.ValidationException ex)
            {
                await HandleValidationExceptionAsync(httpContext , ex);
            }
            catch (Exception ex)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                _logger.LogError($"Exception Occured: {ex.Message}");
                var response = new ErrorDetails()
                {
                    StatusCode = httpContext.Response.StatusCode ,
                    Message = new List<string>() { ex.Message }
                }.ToString();
                _logger.LogError($"Something went wrong: {response}");
                await httpContext.Response.WriteAsync(response);
            }
        }

        private async Task HandleValidationExceptionAsync( HttpContext context , FluentValidation.ValidationException exception )
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var validationErrors = exception.Errors.Select(e => e.ErrorMessage).ToList();
            var errorDetails = new ErrorDetails
            {
                StatusCode = context.Response.StatusCode ,
                Message = validationErrors
            };
            await context.Response.WriteAsync(errorDetails.ToString());
        }
    }
}
