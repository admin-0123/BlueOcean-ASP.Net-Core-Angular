using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Virta.Data.Interfaces;
using Virta.Data;
using Virta.Helpers;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;
using Virta.Api.Services.Interfaces;
using Virta.Api.Services;
using Virta.Services.Interfaces;
using Virta.Services;

namespace Virta.Extensions
{
    public static class AppServiceExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoClient, MongoClient>(
                sp => new MongoClient(configuration.GetConnectionString("MongoDb"))
            );

            services.AddDbContext<DataContext>(
                options => {
                    options.UseLazyLoadingProxies();
                    options.UseNpgsql(configuration.GetConnectionString("PostgreSql"));
                }
            );

            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IAttributesRepository, AttributesRepository>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IAttributesService, AttributesService>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            return services;
        }
    }
}
