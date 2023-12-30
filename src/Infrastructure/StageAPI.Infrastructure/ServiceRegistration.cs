using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using StageAPI.Application.Services;
using StageAPI.Infrastructure.Services;

namespace StageAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        /// <summary>
        /// Adds infrastructure services to the specified
        /// </summary>
        /// <param name="services">The service collection</param>
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            // Register RestClient
            services.AddSingleton<RestClient>();
            // Register the weather service as a scoped service 
            services.AddScoped<IWeatherService, WeatherService>();
        }
    }
}