using Microsoft.Extensions.Logging;
using StageAPI.Application.Repositories.Activity;
using StageAPI.Persistence.Contexts;

namespace StageAPI.Persistence.Repositories.Activity
{
    /// <summary>
    /// Repository for performing read operations on Activity entities
    /// </summary>
    public class ActivityReadRepository : ReadRepository<Domain.Entities.Activity>, IActivityReadRepository
    {
        /// <summary>
        /// Constructor for ActivityReadRepository
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="logger">The logger for logging</param>
        public ActivityReadRepository(StageAPIDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}