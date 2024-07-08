using CleanArch.ATG.API.ErrorHandlers;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace CleanArch.ATG.API.Middlewares
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler( this IApplicationBuilder app , NLog.ILogger logger )
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var response = new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode ,
                            Message = new List<string>() { contextFeature.Error.Message }
                        }.ToString();
                        await context.Response.WriteAsync(response);
                        logger.Error($"Error: {response}");
                    }
                });
            });
        }
    }
}
