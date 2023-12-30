using Microsoft.Extensions.Logging;
using StageAPI.Application.Repositories.Activity;
using StageAPI.Persistence.Contexts;

namespace StageAPI.Persistence.Repositories.Activity
{
    /// <summary>
    /// Repository for performing write operations on Activity entities
    /// </summary>
    public class ActivityWriteRepository : WriteRepository<Domain.Entities.Activity>, IActivityWriteRepository
    {
        /// <summary>
        /// Constructor for ActivityWriteRepository
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="logger">The logger for logging</param>
        public ActivityWriteRepository(StageAPIDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}