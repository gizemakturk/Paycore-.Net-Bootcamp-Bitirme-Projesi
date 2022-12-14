using AutoMapper;
using BackgroundWorker;
using Data.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.AuthenticatedUserServices.Abstract;
using Service.AuthenticatedUserServices.Concrete;
using Service.CategoryService.Abstract;
using Service.CategoryService.Concrete;
using Service.EmailService.Abstract;
using Service.EmailService.Concrete;
using Service.Mapper;
using Service.OfferService.Abstract;
using Service.OfferService.Concrete;
using Service.ProductService.Abstract;
using Service.ProductService.Concrete;
using Service.Token.Abstract;
using Service.Token.Concrete;
using Service.UserService.Abstract;
using Service.UserService.Concrete;
using StackExchange.Redis;
using System;

namespace WebApi.StartUpExtension
{
    public static class ExtensionService
    {
        public static void AddRedisDependencyInjection(this IServiceCollection services, IConfiguration Configuration)
        {
            //redis 
            var configurationOptions = new ConfigurationOptions();
            configurationOptions.EndPoints.Add(Configuration["Redis:Host"], Convert.ToInt32(Configuration["Redis:Port"]));
            int.TryParse(Configuration["Redis:DefaultDatabase"], out int defaultDatabase);
            configurationOptions.DefaultDatabase = defaultDatabase;
            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.ConfigurationOptions = configurationOptions;
            //    options.InstanceName = Configuration["Redis:InstanceName"];
            //});
            MailSettings mailSettings = Configuration.GetSection("MailSettings").Get<MailSettings>();
            services.AddSingleton(mailSettings);
            services.AddScoped<IRabbitMQService, RabbitMQService>();
            services.AddScoped<IEmailService, MailService>();
            services.AddHostedService<Worker>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            // services 
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IOfferService, OfferService>();
          //  services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();

            services.AddScoped<ITokenService, TokenService>();


            // mapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper());
        }
    }
}
