using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace SportsStore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                Configuration["Data:SportStoreProducts:ConnectionString"]));
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddMvc().AddNewtonsoftJson();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseEndpoints(routes =>
            {
                routes.MapControllerRoute(
                    name: "null",
                    pattern: "{category}/Page{productPage:int}",
                    defaults: new
                    {
                        Controller = "Product",
                        action = "List"
                    });

                routes.MapControllerRoute(
                    name: null,
                    pattern: "Page{productPage:int}",
                     defaults: new
                     {
                         Controller = "Product",
                         action = "List",
                         productPage = 1
                     });

                routes.MapControllerRoute(
                   name: null,
                   pattern: "{category}",
                   defaults: new
                   {
                       Controller = "Product",
                       action = "List",
                       productPage = 1
                   });

                routes.MapControllerRoute(
                   name: null,
                   pattern: "",
                   defaults: new
                   {
                       controller = "Product",
                       action = "List",
                       productPage = 1
                   });

                routes.MapControllerRoute(
                   name: null,
                   pattern: "{controller}/{action}/{id?}");

            });


        }
    }
}
