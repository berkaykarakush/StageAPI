using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using StageAPI.Application.Repositories;
using StageAPI.Domain;
using StageAPI.Persistence.Contexts;

namespace StageAPI.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T>, IAsyncDisposable where T : BaseEntity
    {
        private readonly StageAPIDbContext _context;
        private readonly ILogger _logger;

        public WriteRepository(StageAPIDbContext context, ILogger logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        /// <summary>
        /// Expose the DbSet for the entity type T
        /// </summary>
        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T model)
        {
            try
            {
                // Add the entity to the DbSet asynchronously
                EntityEntry<T> entityEntry = await Table.AddAsync(model);
                // Return whether the entity has been added successfully
                return entityEntry.State == EntityState.Added;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding entity to the database");
                throw new Exception("Error adding entity to the database", ex);
            }
        }

        public async Task<bool> AddRangeAsync(List<T> datas)
        {
            try
            {
                // Add a range of entities to the DbSet asynchronously
                await Table.AddRangeAsync(datas);
                // Always return true since DbSet.AddRangeAsync does not return a result
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding a range of entity to the database: {ex.Message}");
                throw new Exception("Error adding a range of entity to the database", ex);
            }
        }



        public bool Remove(T model)
        {
            try
            {
                // Remove the entity from the DbSet
                EntityEntry<T> entityEntry = Table.Remove(model);
                // Return whether the entity has been marked as deleted
                return entityEntry.State == EntityState.Deleted;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error removing entity from the database: {ex.Message}");
                throw new Exception("Error removing entity from the database", ex);
            }
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            try
            {
                // Find the entity with the specified identifier asynchronously
                T model = await Table.FirstOrDefaultAsync(data => data.Id == id);
                // Remove the entity using the synchronous Remove method
                return Remove(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error removing entity from the database: {ex.Message}");
                throw new Exception("Error removing entity from the database", ex);
            }
        }

        public bool RemoveRange(List<T> datas)
        {
            try
            {
                // Remove a range of entities from the DbSet
                Table.RemoveRange(datas);
                // Always return true since DbSet.RemoveRange does not return a result
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error removing a range of entity from the database: {ex.Message}");
                throw new Exception("Error removing a range of entity from the database", ex);
            }
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error saving changes to the database: {ex.Message}");
                throw new("Error saving changes to the database", ex);
            }
        }
        public ValueTask DisposeAsync() => _context.DisposeAsync();
    }
}