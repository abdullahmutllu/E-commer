using BLL.IoC;
using DAL.Concrete.EntityFramework.Database;
using DAL.SeedData;
using ENTÝTY.Concrete.POCO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_UI.CustomValidaiton;

namespace Web_UI
{
    public class Startup
    {
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();//MVC
            services.Dependency();//BLL IoC
            services.AddIdentity<AppUser, AppRole>(opt => 
            {
                opt.SignIn.RequireConfirmedEmail = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 5;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = false;
                opt.User.RequireUniqueEmail = true;
                opt.User.AllowedUserNameCharacters = "abcçdefghiýjklmnoöpqrsþtuüvwxyzABCÇDEFGHIÝJKLMNOÖPQRSÞTUÜVWXYZ0123456789-._@+";
            })
                .AddEntityFrameworkStores<MyProjectDbContext>()
                .AddErrorDescriber<AppUserValiadion>();
        }

       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // MyClass.Seed();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                  name: "AdminArea",
                  areaName: "Admin",
                  pattern: "Admin/{controller=Admin}/{action=Index}");

                endpoints.MapDefaultControllerRoute();

                
            });
        }
    }
}
