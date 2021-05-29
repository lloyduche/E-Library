using EBookLibrary.DataAccess.Abstractions;
using EBookLibrary.DataAccess.Implementations;
using EBookLibrary.Models;
using EBookLibrary.Models.Settings;
using EBookLibrary.Server.Core.Abstractions;
using EBookLibrary.Server.Core.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace EBookLibrary.Presentation.DIServices
{
    public static class DIServices
    {

        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFileUpload, FileUpload>();
            services.AddTransient<IMailService, MailService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAuthService, AuthService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApplicationBaseAddress>(configuration.GetSection("ApplicationBaseAddress"));
            services.Configure<CloudinaryConfig>(configuration.GetSection("CloudinaryConfig"));
            services.Configure<MailConfig>(configuration.GetSection("MailConfig"));
            services.Configure<JWTData>(configuration.GetSection("JWTConfig"));
           
        }
    }
}
