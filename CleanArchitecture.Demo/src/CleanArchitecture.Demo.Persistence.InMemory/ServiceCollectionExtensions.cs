using CleanArchitecture.Demo.Application;
using CleanArchitecture.Demo.Persistence.InMemory.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Demo.Persistence.InMemory
{
    public static class ServiceCollectionExtensions
    {

        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
        }
    }
}
