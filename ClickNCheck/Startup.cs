﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ClickNCheck.Data;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Hangfire;
using Hangfire.Dashboard;
using ClickNCheck.Services;
using Microsoft.AspNetCore.Owin;
using ClickNCheck.Controllers;

namespace ClickNCheck
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
            services.AddDbContext<ClickNCheckContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

             services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
        


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            //services.AddMvc()
            //    .AddJsonOptions(
            //        options => options.SerializerSettings.ReferenceLoopHandling =
            //        Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //    );
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
       public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors(o => o.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader());

            app.UseHttpsRedirection();
            if (Configuration["EnableCORS"] == "True")
            {

            }

            app.UseHangfireServer();
            app.UseHangfireDashboard();
   

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseAuthentication();

            app.UseMvc();
            VerificationChecking verificationChecking = new VerificationChecking();
            Jobs jobs = new Jobs();
            RecurringJob.AddOrUpdate(() => verificationChecking.doStuff(), Cron.MinuteInterval(30));
            RecurringJob.AddOrUpdate(() => jobs.CheckUserPasswords(), Cron.Daily);

            
            //Solution to hangfire not working online
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() }
            });
        }

        public class MyAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context)
            {
                var httpContext = context.GetHttpContext();

                // Allow all authenticated users to see the Dashboard (potentially dangerous).
                return httpContext.User.Identity.IsAuthenticated;
            }
        }
    }
}