using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Virta.Data.Interfaces;
using Virta.Data;
using VirtaApi.Helpers;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;
using Virta.Api.Services.Interfaces;
using Virta.Api.Services;

namespace Virta.Extensions
{
    public static class AppServiceExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("MySql");

            services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(configuration.GetConnectionString("MongoDb")));

            services.AddDbContext<DataContext>(
                options => {
                    options.UseLazyLoadingProxies();
                    options.UseMySql(
                        connectionString,
                        ServerVersion.AutoDetect(connectionString)
                    );
                }
            );

            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            return services;
        }
    }
}
