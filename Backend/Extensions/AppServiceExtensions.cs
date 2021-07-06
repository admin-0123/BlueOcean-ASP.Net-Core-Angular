using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Virta.Api.Services;
using Virta.Api.Services.Interfaces;
using Virta.Data;
using Virta.Data.Interfaces;
using Virta.Helpers;
using Virta.Services;
using Virta.Services.Interfaces;

namespace Virta.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AddAppServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddSingleton<IMongoClient, MongoClient>(
                sp => new MongoClient(configuration.GetConnectionString("MongoDb"))
            );

            services.AddScoped(c =>
		        c.GetService<IMongoClient>().StartSession());

            services.AddDbContext<DataContext>(
                options =>
                {
                    options.UseLazyLoadingProxies();
                    options.UseNpgsql(configuration.GetConnectionString("PostgreSql"));
                }
            );

            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IAttributesRepository, AttributesRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IWishlistRepository, WishlistRepository>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IAttributesService, AttributesService>();
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            return services;
        }
    }
}
