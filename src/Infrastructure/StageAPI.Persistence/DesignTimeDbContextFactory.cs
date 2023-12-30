using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using StageAPI.Persistence.Contexts;

namespace StageAPI.Persistence
{
    // This class is used for design-time DbContext creation, typically when running Entity Framework Core tools (e.g., migrations)
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<StageAPIDbContext>
    {
        // This method is called by EF Core tools to create a new instance of the DbContext during design-time operations.
        public StageAPIDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/StageAPI.WebAPI")).AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("PostgreSQLConnection");
            DbContextOptionsBuilder<StageAPIDbContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<StageAPIDbContext>();
            dbContextOptionsBuilder.UseNpgsql(connectionString);
            return new(dbContextOptionsBuilder.Options);
        }
    }
}