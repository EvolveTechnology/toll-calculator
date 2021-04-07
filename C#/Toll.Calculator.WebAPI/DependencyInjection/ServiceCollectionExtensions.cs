using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Toll.Calculator.DAL;
using Toll.Calculator.DAL.Interface;
using Toll.Calculator.Infrastructure;
using Toll.Calculator.Service;

namespace Toll.Calculator.WebAPI.DependencyInjection
{
    public static class ServiceCollectionExtensionsS
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddSingleton<IVehicleRepository, VehicleRepository>()
                .AddSingleton<ITollFeeRepository, TollFeeRepository>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<ITollFeeService, TollFeeService>();
        }

        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<TollFreeVehicleOptions>(configuration.GetSection("Options"))
                .Configure<FeeTimeZonesOptions>(configuration.GetSection("Options"));
        }
    }
}