using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using VirtaApi.Data.Interfaces;
using VirtaApi.Data;
using VirtaApi.Models;

namespace VirtaApi.Extensions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<DataContext>();

            // services.Configure<IdentityOptions>(options =>
            // {
            //     // Password settings.
            //     options.Password.RequireDigit = true;
            //     options.Password.RequireLowercase = true;
            //     options.Password.RequireNonAlphanumeric = true;
            //     options.Password.RequireUppercase = true;
            //     options.Password.RequiredLength = 6;
            //     options.Password.RequiredUniqueChars = 1;

            //     // Lockout settings.
            //     options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //     options.Lockout.MaxFailedAccessAttempts = 5;
            //     options.Lockout.AllowedForNewUsers = true;

            //     // User settings.
            //     options.User.AllowedUserNameCharacters =
            //     "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            //     options.User.RequireUniqueEmail = false;
            // });

            // services.ConfigureApplicationCookie(options =>
            // {
            //     // Cookie settings
            //     options.Cookie.HttpOnly = true;
            //     options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

            //     options.LoginPath = "/Identity/Account/Login";
            //     options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            //     options.SlidingExpiration = true;
            // });

            return services;
        }
    }
}
