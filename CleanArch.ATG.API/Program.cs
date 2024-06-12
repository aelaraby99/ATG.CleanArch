using CleanArch.ATG.Application;
using CleanArch.ATG.Infrastructure;
using CleanArch.ATG.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore.Storage;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CleanArch.ATG.API
{
    public class Program
    {
        public static void Main( string [] args )
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddApiServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                //app.UseSwaggerUI();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json" , "API V1"); //Select a definition
                    c.SwaggerEndpoint("/swagger/v2/swagger.json" , "API V2");
                });

                //dotnet ef migrations add InitialCreate--context ATGIdentityDbContext --output - dir Migrations / ATGIdentityDbContextMigrations

                //dotnet ef database update --context ATGIdentityDbContext


            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
