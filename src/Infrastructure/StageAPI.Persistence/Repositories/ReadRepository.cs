using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StageAPI.Application.Repositories;
using StageAPI.Domain;
using StageAPI.Persistence.Contexts;

namespace StageAPI.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly StageAPIDbContext _context;
        private readonly ILogger _logger;

        public ReadRepository(StageAPIDbContext context, ILogger logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); ;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        {
            // Start with the IQueryable representation of the DbSet
            var query = Table.AsQueryable();
            // Disable entity tracking if specified
            if (!tracking)
                query = query.AsNoTracking();
            // Return the resulting IQueryable
            return query;
        }

        public async Task<T> GetByIdAsync(Guid id, bool tracking = true)
        {
            // Start with the IQueryable representation of the DbSet
            var query = Table.AsQueryable();
            // Disable entity tracking if specified
            if (!tracking)
                query = query.AsNoTracking();
            try
            {
                // Return the first entity with the specified identifier
                return await query.FirstOrDefaultAsync(data => data.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting entity by id from the database: {ex.Message}");
                throw new Exception("Error getting entity by id from the database", ex);
            }
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            // Start with the IQueryable representation of the DbSet
            var query = Table.AsQueryable();
            // Disable entity tracking if specified
            if (!tracking)
                query = query.AsNoTracking();
            try
            {
                // Return the first entity that satisfies the provided filter
                return await query.FirstOrDefaultAsync(method);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting a single entity from the database: {ex.Message}");
                throw new Exception("Error getting a single entity from the database", ex);
            }

        }
    }
}