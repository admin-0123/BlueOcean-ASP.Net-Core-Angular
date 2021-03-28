using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using VirtaApi.Data.Interfaces;
using VirtaApi.Data;
using VirtaApi.Helpers;

namespace VirtaApi.Extensions
{
    public static class AppServiceExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            return services;
        }
    }
}
