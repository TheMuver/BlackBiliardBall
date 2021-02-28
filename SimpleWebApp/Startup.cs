using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //
                endpoints.MapGet("/", async context =>
                {
                    string page = File.ReadAllText("Site/homePage.html");
                    await context.Response.WriteAsync(page);
                });

                endpoints.MapGet("/admin", async context =>
                {
                    string page = File.ReadAllText("Site/adminPage.html");
                    await context.Response.WriteAsync(page);
                });

                endpoints.MapGet("/prediction", async context =>
                {
                    string page = File.ReadAllText("Site/predictionPage.html");
                    await context.Response.WriteAsync(page);
                });

                //
                endpoints.MapGet("/randomPrediction", async context =>
                {
                    // Будут проблемы
                    PredicitionsManager pm = new PredicitionsManager();
                    await context.Response.WriteAsync(pm.GetRandomPrediction());
                });

                endpoints.MapGet("/login", async context => 
                {
                    string login = "admin";
                    await context.Response.WriteAsync(login);
                });

                endpoints.MapGet("/password", async context =>
                {
                    string login = "admin";
                    await context.Response.WriteAsync(login);
                });

                endpoints.MapGet("/addPrediction", async context =>
                {
                    PredicitionsManager pm = new PredicitionsManager();
                    //var query = context.Request.Query;
                });
            });
        }
    }
}
