using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Mvc_Auth.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pickpoint_delivery_service;

namespace Mvc_Auth
{
    public class IdentityAppStartup
    {
        public IConfiguration Configuration { get; }

        public IdentityAppStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public static void ConfigureServices(IServiceCollection services,IConfiguration Configuration)
        {
            services.AddDbContext<WebApplicationDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            DeliveryDbContext.ConfigureDeliveryServices(services, Configuration);
            services.AddDefaultIdentity<IdentityUser>(options =>
            {                                
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;
 
            })
            .AddEntityFrameworkStores<WebApplicationDbContext>();
            services
                .AddRazorPages(ConfigureRazorPages)
                .AddRazorOptions(ConfigureRazorOptions)
                .AddApplicationPart(typeof(LoginModel).Assembly)

                .AddRazorRuntimeCompilation(ConfigureRuntime);
            services
                .AddControllersWithViews(ConfigureMvc)
                .AddApplicationPart(typeof(LoginModel).Assembly)
                .AddRazorOptions(ConfigureRazorOptions)
                .AddRazorRuntimeCompilation(ConfigureRuntime);
            MvcLoginAppStartup.ConfigureMvcLoginAppServicesServices(Configuration,services);
        }

        private static void ConfigureRazorPages(RazorPagesOptions obj)
        {
        }

        private static void ConfigureRuntime(MvcRazorRuntimeCompilationOptions obj)
        {
        }

        private static void ConfigureMvc(MvcOptions obj)
        {
        }

        private static void ConfigureRazorOptions(RazorViewEngineOptions razorViewEngineOptions)
        {
            //razorViewEngineOptions.AreaPageViewLocationFormats
            //razorViewEngineOptions.ViewLocationExpanders
            //razorViewEngineOptions.PageViewLocationFormats
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            /*app.Use(async (http, next) =>
            {
                if (http.Request.Path.ToString() != "/Identity/Account/Login")
                {
                    if (http.User.Identity.IsAuthenticated == false)
                    {
                        
                        http.Response.Redirect("/Identity/Account/Login");
                        

                    }
                    else
                    {
                        await next.Invoke();
                    }
                }
                else
                {
                    await next.Invoke();
                }

            });*/
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapFallbackToController("Fallback", "Home");
            });
        }
    }
}
