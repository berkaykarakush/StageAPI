using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StageAPI.Application.UnitOfWorks;
using StageAPI.Persistence.Contexts;
using StageAPI.Persistence.UnitOfWorks;

namespace StageAPI.Persistence
{
    public static class ServiceRegistration
    {
        /// <summary>
        /// Adds persistence services to the specified <paramref name="services"/> collection
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration containing the database connection string</param>
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure the database context using Npgsql with the specified PostgreSQL connection string
            services.AddDbContext<StageAPIDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreSQLConnection")));
            // Register the unit of work as a scoped service
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}