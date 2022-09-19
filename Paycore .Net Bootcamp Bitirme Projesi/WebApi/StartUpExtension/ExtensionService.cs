using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.CategoryService.Abstract;
using Service.CategoryService.Concrete;
using Service.Mapper;
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
        }

        public static void AddServices(this IServiceCollection services)
        {
            // services 
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
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
