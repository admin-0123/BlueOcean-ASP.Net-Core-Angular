using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using VirtaApi.Data.Interfaces;
using VirtaApi.Data;

namespace VirtaApi.Extensions
{
    public static class AppServiceExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductsRepository, ProductsRepository>();

            return services;
        }
    }
}
