using CleanArch.ATG.Application.Features.Common.Behaviors;
using CleanArch.ATG.Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace CleanArch.ATG.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices( this IServiceCollection services )
        {
            services.AddMediatR(cnf => cnf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddSingleton(typeof(IPipelineBehavior<,>) , typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>) , typeof(ValidationBehavior<,>));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
