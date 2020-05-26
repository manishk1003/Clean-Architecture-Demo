using System;
using AutoMapper;
using CleanArchitecture.Demo.WebApi.Presenters;
using CleanArchitecture.Demo.WebApi.Translator;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Demo.WebApi.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static void AddApiServices(this IServiceCollection services)
        {
            services.AddTransient<IAddProductPresenter, AddProductPresenter>();
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            var config = new MapperConfiguration(c => {
                c.AddProfile<MapperProfile>();
             
            });
            services.AddSingleton<IMapper>(s => config.CreateMapper());
        }

    }
}
