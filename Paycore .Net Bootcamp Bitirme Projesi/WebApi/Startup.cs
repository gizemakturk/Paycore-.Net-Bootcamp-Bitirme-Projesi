using Base.JWT;
using Data.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Service.EmailService.Abstract;
using Service.EmailService.Concrete;
using System.Net;
using WebApi.StartUpExtension;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static JwtConfig JwtConfig { get; private set; }


        public void ConfigureServices(IServiceCollection services)
        {
            //cashe in memory
            services.AddMemoryCache();


            //response cashe  on services
            services.AddControllersWithViews(options =>
                         options.CacheProfiles.Add("Profile45", new CacheProfile
                         {
                             Duration = 45
                         }));


            // hibernate
            var connStr = Configuration.GetConnectionString("PostgreSqlConnection");
            services.AddNHibernatePosgreSql(connStr);

            // Configure JWT Bearer
            JwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            //Mail settings
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddScoped<IEmailService, MailService>();
            services.AddScoped<IRabbitMQService, RabbitMQService>();

            // service
            services.AddServices();
            services.AddJwtBearerAuthentication();
            services.AddCustomizeSwagger();
            services.AddRedisDependencyInjection(Configuration);


            //services.AddMvc(options => {
            //    options.Filters.Add(typeof(ResponseGiudAttribute));
            //});

            services.AddControllers();

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PycApi Tech Company"));
            }


            // middleware
            //app.UseMiddleware<HeartbeatMiddleware>();
            //app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();


            // add auth 
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}