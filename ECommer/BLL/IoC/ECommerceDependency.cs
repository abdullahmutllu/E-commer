using BLL.Abstarct;
using BLL.Conctere;
using DAL.Abstract;
using DAL.Concrete.EntityFramework;
using DAL.Concrete.EntityFramework.Database;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IoC
{
    public static class ECommerceDependency
    {
        public static IServiceCollection Dependency(this IServiceCollection services)
        {
            //services.AddSingleton();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IProductDAL, EfProductRepo>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDAL, EfCategoryRepo>();
            services.AddScoped<IAccountDAL, EfAccountRepo>();
            services.AddScoped<IAccountService, AccountManager>();
            services.AddScoped<IBasketDAL, EfBasketRepo>();
            services.AddScoped<IBasketService, BasketManager>();
            //services.AddScoped<GelsinBanaDbContext>();//DbContext Çevrilecek
            services.AddDbContext<MyProjectDbContext>();

            //services.AddTransient();
            return services;
        }
    }
}
