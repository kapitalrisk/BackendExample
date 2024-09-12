using InMemoryDatabase;
using Stock.Repositories;
using Stock.Services;
using Stock.Services.HostedServices;

namespace Stock
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddStockServices(this IServiceCollection services)
        {
            services.AddInMemoryDatabase();

            // Add Services
            services.AddScoped<IProductService, ProductService>();
            services.AddHostedService<LowStockAlertingHostedService>();

            // Add Repos
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
