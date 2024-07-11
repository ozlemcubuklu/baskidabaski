
using Business.Abstract;
using Business.Concrete;
using Data.Abstract;
using Data.Concrete.EfCore;
using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Container
{
    public static class Extensions
    {
        public static void ContainerDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICartService,CartManager>();
            services.AddScoped<ICartRepository, EfCoreCartRepository>();


            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryRepository, EfCoreCategoryRepository>();



            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<IOrderRepository, EfCoreOrderRepository>();

            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IProductRepository,EfCoreProductRepository>();



            






        }
    }
}
