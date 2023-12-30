using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StageAPI.Application.Features.Commands.Activity.CreateActivity;
using StageAPI.Application.Mapping;

namespace StageAPI.Application
{
    public static class ServiceRegistration
    {
        /// <summary>
        /// Adds application-specific services to the IServiceCollection.
        /// This includes configuring and registering services such as AutoMapper
        /// </summary>
        /// <param name="services">The IServiceCollection to which services will be added</param>
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Add AutoMapper
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            // Add MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateActivityCommandHandler).Assembly));
        }
    }
}