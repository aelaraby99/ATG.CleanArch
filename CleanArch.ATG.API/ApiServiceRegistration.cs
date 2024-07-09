using Asp.Versioning;
using CleanArch.ATG.Application.Interfaces.JWT;
using CleanArch.ATG.Domain.Entities.Identity;
using CleanArch.ATG.Infrastructure.Contexts;
using CleanArch.ATG.Infrastructure.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using System.Text;

namespace CleanArch.ATG.API
{
    public static class ApiServiceRegistration
    {
        public static IServiceCollection AddApiServices( this IServiceCollection services , IConfiguration configuration )
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1" , new OpenApiInfo { Title = "Api version 1 test" , Version = "v1" }); // The title and version of the API
                c.SwaggerDoc("v2" , new OpenApiInfo { Title = "My API" , Version = "v2" });
            });

            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1 , 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = new UrlSegmentApiVersionReader(); //if only in the path
            }).AddApiExplorer(c =>
            {
                c.GroupNameFormat = "'v'VVV";
                c.SubstituteApiVersionInUrl = true;
            });
            //services.AddDbContext<ATGDbContext>
            //    (options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ,
            //    b => b.MigrationsAssembly(typeof(ATGDbContext).Assembly.FullName)));
            services.AddDbContext<ATGDbContext>
                (options => options.UseOracle(configuration.GetConnectionString("OracleConnection") ,
                b => b.MigrationsAssembly(typeof(ATGDbContext).Assembly.FullName)));

            //services.AddDbContext<ATGIdentityDbContext>
            //    (options => options.UseOracle(configuration.GetConnectionString("OracleConnection") ,
            //     b => b.MigrationsAssembly(typeof(ATGIdentityDbContext).Assembly.FullName)));
            //services.AddDefaultIdentity<UserApplication>()
            //  .AddEntityFrameworkStores<ATGIdentityDbContext>();
            services.AddIdentity<UserApplication , AppRole>()
                .AddEntityFrameworkStores<ATGDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IJwtTokenService , JwtTokenService>();

            var key = Encoding.ASCII.GetBytes(configuration ["Jwt:Key"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = NegotiateDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = NegotiateDefaults.AuthenticationScheme;
                x.DefaultScheme = NegotiateDefaults.AuthenticationScheme;
            }).AddNegotiate()
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true ,
                    IssuerSigningKey = new SymmetricSecurityKey(key) ,
                    ValidateIssuer = false ,
                    ValidateAudience = false
                };
            });
            //services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = options.DefaultPolicy;
            });

            // Add NLog as the logging provider
            services.AddLogging(loggingBuilder =>
            {
                //loggingBuilder.ClearProviders();
                loggingBuilder.AddNLog(); // Add NLog as the logging provider
            });
            return services;
        }
    }
}
