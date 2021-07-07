using EBookLibrary.DataAccess;
using EBookLibrary.DataAccess.DataSeed;
using EBookLibrary.Models;
using EBookLibrary.Presentation.APIExceptionMiddleWare;
using EBookLibrary.Presentation.DIServices;
using EBookLibrary.Presentation.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog.Extensions.Logging;

using System;

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
            services.AddDistributedMemoryCache();
            services.AddHttpContextAccessor();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(120);//You can set Time
            }); services.AddConfigurations(Configuration);
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
                app.UseExceptionHandler("/Error");
            }

            app.UseMiddleware<ApiExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();



            app.UseSession();
            app.Use(async (ctx, next) =>
            {
                var jwt = ctx.Session.GetString("access_token");
                if (!string.IsNullOrWhiteSpace(jwt))
                {
                    ctx.Request.Headers.Add("Authorization", "Bearer " + jwt);
                }
                await next();
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            Seeder.Seed(context, roleManager, userManager).Wait();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
