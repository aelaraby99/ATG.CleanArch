using CleanArch.ATG.Application.Interfaces;
using CleanArch.ATG.Infrastructure.Data;
using CleanArch.ATG.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.ATG.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices( this IServiceCollection services )
        {
            services.AddSingleton<IFakeDataRepo , FakeDataRepo>();
            services.AddScoped<IUnitOfWork , UnitOfWork>();
            return services;
        }
    }
}
