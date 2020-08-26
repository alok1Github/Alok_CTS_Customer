using AutoMapper;
using Customer.DataAccess.Data;
using Customer.API.Features;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Customer.API.ExceptionHandlers;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Customer.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IGetCustomers, SearchCustomers>();
            services.AddScoped<IGetCustomers, GetCustomers>();
            services.AddScoped<IPostCustomer, PostCustomer>();
            services.AddScoped<IPutCustomer, PutCustomers>();
            services.AddScoped<IDeleteCustomer, DeleteCustomers>();
            services.AddScoped<ValidationHandler>();
            services.AddAutoMapper(typeof(Startup));

            services.AddMvc()
                    .AddJsonOptions(opts =>
                    {
                        opts.JsonSerializerOptions.Converters.Add(new DateTimeConverter());

                    });

            services.AddMvc(o => o.EnableEndpointRouting = false);

            services.AddApiVersioning(option =>
            {
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.ReportApiVersions = true;
                option.DefaultApiVersion = new ApiVersion(1, 0);
                option.ApiVersionReader = new QueryStringApiVersionReader("version");
            });

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


            app.UseExceptionHandler(a => GobalHandler(a));

            app.UseRouting();
            app.UseDefaultFiles();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Get}/{id?}");
            });
        }

        private static void GobalHandler(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                context.Response.ContentType = "application/json";
                var result = JsonConvert.SerializeObject(ExceptionResponseBuilder.createRespone(exceptionHandlerPathFeature.Error, context));
                await context.Response.WriteAsync(result);
            });
        }
    }
}
