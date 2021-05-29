using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookLibrary.Presentation.Extensions
{
    public static class Authentication
    {
        public static void AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("JWTKey:JWTSecurityKey").Value)), 
                    ValidateIssuer = true,
                    ValidIssuer = configuration.GetSection("JWTKey:Issuer").Value,
                    ValidateAudience = true,
                    ValidAudience = configuration.GetSection("JWT:Issuer").Value
                };
            });
        }
    }

}
