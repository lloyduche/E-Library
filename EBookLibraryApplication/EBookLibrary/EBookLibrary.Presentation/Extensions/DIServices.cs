﻿using AutoMapper;
using EBookLibrary.Commons.Profiles;
using EBookLibrary.Client.Core.Implementations;
using EBookLibrary.DataAccess.Abstractions;
using EBookLibrary.DataAccess.Implementations;
using EBookLibrary.Models;
using EBookLibrary.Models.Settings;
using EBookLibrary.Server.Core.Abstractions;
using EBookLibrary.Server.Core.Implementations;
using EBookLibrary.ViewModels.UserVMs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EBookLibrary.Presentation.DIServices
{
    public static class DIServices
    {

        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFileUpload, FileUpload>();
            services.AddTransient<IMailService, MailService>();
            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAuthService, AuthService>();
            services.AddCustomConfiguredAutoMapper();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            services.AddScoped<IAppHttpClient, AppHttpClient>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IBookService, BookService>();
        }

        public static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApplicationBaseAddress>(configuration.GetSection("ApplicationBaseAddress"));
            services.Configure<CloudinaryConfig>(configuration.GetSection("CloudinaryConfig"));
            services.Configure<MailConfig>(configuration.GetSection("SmtpConfig"));
            services.Configure<JWTData>(configuration.GetSection("JWTConfigurations"));
           
        }
    }

    public static class CustomAutoMapper
    {
        public static void AddCustomConfiguredAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UsersProfile());
            });

            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
