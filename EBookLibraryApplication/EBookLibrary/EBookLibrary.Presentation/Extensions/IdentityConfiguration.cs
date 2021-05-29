using EBookLibrary.DataAccess;
using EBookLibrary.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EBookLibrary.Presentation.Extensions
{
    public static class IdentityConfiguration
    {
        public static void AddIdentityConfigurations(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
            });

            services.AddIdentity<User, IdentityRole>()
             .AddEntityFrameworkStores<AppDbContext>()
             .AddDefaultTokenProviders();
        }
    }
}
