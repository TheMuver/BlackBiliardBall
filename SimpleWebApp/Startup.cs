using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace SimpleWebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<PredicitionsManager>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => options.LoginPath = new PathString("/Auth"));
            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapGet("/auth", async context =>
                {
                    string page = File.ReadAllText("Site/loginPage.html");
                    await context.Response.WriteAsync(page);
                });

                endpoints.MapPost("/login", async context =>
                {
                    var credentials = await context.Request.ReadFromJsonAsync<Credentials>();
                    // с заданным логином и паролем мы пойдем в базу
                    // если в базе есть пользователь, то всё ок, если нет, то ничего не делаем
                    var fakeUser = new Credentials { Login = "admin", Password = "admin" };
                    if (credentials.Login == fakeUser.Login && credentials.Password == fakeUser.Password)
                    {
                        List<Claim> claims = new List<Claim>()
                        {
                            new Claim(ClaimsIdentity.DefaultNameClaimType, credentials.Login)
                        };
                        // создаем объект ClaimsIdentity
                        ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

                        // перенаправляем на нужную сраницу
                        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

                        context.Response.Redirect("/admin");
                    }

                    await context.Response.WriteAsync(credentials.Login);
                    
                });

                endpoints.MapGet("/admin", async context =>
                {
                    string page = File.ReadAllText("Site/adminPage.html");
                    await context.Response.WriteAsync(page);
                }).RequireAuthorization();

                endpoints.MapGet("/", async context =>
                {
                    string page = File.ReadAllText("Site/predictionPage.html");
                    await context.Response.WriteAsync(page);
                });

                endpoints.MapGet("/randomPrediction", async context =>
                {
                    PredicitionsManager predicitionsManager = app.ApplicationServices.GetService<PredicitionsManager>();
                    await context.Response.WriteAsync(predicitionsManager.GetRandomPrediction().PredictionText);
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
                        s = (await sr.ReadToEndAsync()).Split("::");
                    app.ApplicationServices.GetService<PredicitionsManager>().UpdatePrediction(new Prediction(int.Parse(s[0]), s[1]));
                });
            });
        }
    }
}
