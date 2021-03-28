using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            services.AddSingleton<PredicitionsManager>();
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

                endpoints.MapGet("/randomPrediction", async context =>
                {
                    PredicitionsManager predicitionsManager = app.ApplicationServices.GetService<PredicitionsManager>();
                    await context.Response.WriteAsync(predicitionsManager.GetRandomPrediction().prediction);
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

                endpoints.MapPost("/addPrediction", async context =>
                {
                    Prediction p = await context.Request.ReadFromJsonAsync<Prediction>();
                    app.ApplicationServices.GetService<PredicitionsManager>().AddPrediction(p);
                });

                endpoints.MapGet("/getAllPredictions", async context => {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(app.ApplicationServices.GetService<PredicitionsManager>().GetAllPreditictions());
                });

                endpoints.MapPost("/deletePrediction", async context => {
                    Prediction p = await context.Request.ReadFromJsonAsync<Prediction>();
                    app.ApplicationServices.GetService<PredicitionsManager>().DeletePrediction(p);
                });

                endpoints.MapPost("/updatePrediction", async context => {
                    string[] s;
                    using (var sr = new StreamReader(context.Request.BodyReader.AsStream()))
                        s = sr.ReadToEnd().Split("::");
                    app.ApplicationServices.GetService<PredicitionsManager>().UpdatePrediction(int.Parse(s[0]), s[1]);
                });
            });
        }
    }
}
