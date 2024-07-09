using CleanArch.ATG.API.ErrorHandlers;
using CleanArch.ATG.API.Middlewares;
using CleanArch.ATG.Application;
using CleanArch.ATG.Domain.Entities.Identity;
using CleanArch.ATG.Infrastructure;
using CleanArch.ATG.Infrastructure.Contexts;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using NLog;
using NLog.Web;
using System.Net;

namespace CleanArch.ATG.API
{
    public class Program
    {
        public static void Main( string [] args )
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.

               
                builder.Services.AddControllers();
                builder.Services.AddApiServices(builder.Configuration);
                builder.Services.AddApplicationServices();
                builder.Services.AddInfrastructureServices();
                //builder.Logging.ClearProviders();
                //builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                //builder.Host.UseNLog();  // This sets up NLog for Dependency Injection

                var app = builder.Build();

                using var scope = app.Services.CreateScope();

                var services = scope.ServiceProvider;

                // Get your DbContext instances
                var dbContext = services.GetRequiredService<ATGDbContext>();
                var userManager = services.GetRequiredService<UserManager<UserApplication>>();
                var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

                //DataSeeding.SeedData(builder.Configuration , userManager , roleManager);

                // Configure the HTTP request pipeline.
                app.UseSwagger();
                //app.UseSwaggerUI();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json" , "API V1"); //Select a definition
                    c.SwaggerEndpoint("/swagger/v2/swagger.json" , "API V2");
                });

                app.UseHttpsRedirection();

                app.UseStaticFiles();

                app.UseRouting();

                app.UseAuthentication();
                
                app.UseAuthorization();
                app.UseMiddleware<ExceptionMiddleware>();
                //app.ConfigureExceptionHandler(logger);

                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex , "Stopped program because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }
    }
}
