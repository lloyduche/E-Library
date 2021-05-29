
using EBookLibrary.Server.Core;
using EBookLibrary.DataAccess;
using EBookLibrary.DataAccess.DataSeed;
using EBookLibrary.Models;
using EBookLibrary.DataAccess.Abstractions;
using EBookLibrary.DataAccess.Implementations;
using EBookLibrary.Models.Settings;
using EBookLibrary.Presentation.APIExceptionMiddleWare;
using EBookLibrary.Presentation.DIServices;
using EBookLibrary.Presentation.Extensions;
using EBookLibrary.Server.Core.Abstractions;
using EBookLibrary.Server.Core.Implementations;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

using NLog.Extensions.Logging;

using System;
using Microsoft.AspNetCore.Identity;

namespace EBookLibrary.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddHttpClient();
            services.AddAuthenticationConfiguration(Configuration);
            services.AddDbContextPool<AppDbContext>
                (options => options.UseSqlite
                (Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentityConfigurations();
            services.AddServices(Configuration);
            services.AddConfigurations(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory,
            AppDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddNLog();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseMiddleware<ApiExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            Seeder.Seed(context, roleManager, userManager).Wait();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
