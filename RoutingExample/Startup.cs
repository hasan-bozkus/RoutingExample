using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RoutingExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutingExample
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name:"NewsList",
                    pattern:"haberlistesi/{Controller}/{Action}",
                    defaults: new {controller = "News", action = "List" });

                endpoints.MapControllerRoute(
                    name: "havadurumu",
                    pattern:"hava-durumu/{cityID?}",
                    defaults: new {controller = "Weather", action = "Index" },
                    constraints: new
                    {
                        cityID = new CustomConstraint()
                    });

                endpoints.MapControllerRoute(
                    name: "Customers",
                    pattern: "musteriler",
                    defaults: new { controller = "Customer", aciton = "List" });

                endpoints.MapControllerRoute(
                    name: "Customer",
                    pattern: "musteri/{id}",
                    defaults: new { controller = "Customer", action = "Get" });
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
