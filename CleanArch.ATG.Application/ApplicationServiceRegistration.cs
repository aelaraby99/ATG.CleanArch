using CleanArch.ATG.Application.Features.ProductFeatures.Behaviors;
using CleanArch.ATG.Application.Interfaces;
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
            services.AddSingleton(typeof(IPipelineBehavior<,>) , typeof(LoggingBehavior<,>));
            return services;
        }
    }
}
