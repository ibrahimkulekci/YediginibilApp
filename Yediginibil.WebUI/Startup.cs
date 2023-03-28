using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YediginiBil.Business.Abstract;
using YediginiBil.Business.Concrete;
using YediginiBil.DataAccess.Abstract;
using YediginiBil.DataAccess.Concrete.EfCore;

namespace Yediginibil.WebUI
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
            services.AddScoped<IProductDal, EfCoreProductDal>();
            services.AddScoped<IProductService, ProductManager>();

            services.AddScoped<IBrandDal, EfCoreBrandDal>();
            services.AddScoped<IBrandService, BrandManager>();

            services.AddScoped<IIngredientDal, EfCoreIngredientDal>();
            services.AddScoped<IIngredientService, IngredientManager>();

            services.AddScoped<IProductIngredientDal, EfCoreProductIngredientDal>();
            services.AddScoped<IProductIngredientService, ProductIngredientManager>();

            services.AddScoped<IPageDal, EfCorePageDal>();
            services.AddScoped<IPageService, PageManager>();

            services.AddScoped<IBlogDal, EfCoreBlogDal>();
            services.AddScoped<IBlogService, BlogManager>();


            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}/{seourl?}");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
