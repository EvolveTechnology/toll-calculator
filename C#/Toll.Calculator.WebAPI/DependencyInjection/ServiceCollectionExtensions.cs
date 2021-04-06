using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using Toll.Calculator.DAL;
using Toll.Calculator.Infrastructure;
using Toll.Calculator.Service;

namespace Toll.Calculator.WebAPI
{
    public static class ServiceCollectionExtensionsS
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddSingleton<IVehicleRepository, VehicleRepository>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<ITollFeeService, TollFeeService>();
        }

        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var tollFreeVehicles = configuration.GetSection("Options:TollFreeVehicles").Get<List<string>>();
            
            services = services
                .Configure<TollFreeVehicleOptions>(configuration.GetSection("Options"));

            return services;
        }
    }
}