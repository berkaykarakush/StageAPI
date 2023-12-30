using System.Linq.Expressions;
using StageAPI.Domain;

namespace StageAPI.Application.Repositories
{
    /// <summary>
    /// Generic repository interface for read operations (retrieve data) on entities.
    /// </summary>
    /// <typeparam name="T">The type of entity that the repository operates on</typeparam>
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Gets all entities from the repository
        /// </summary>
        /// <param name="tracking">Indicates whether change tracking is enabled (default is true)</param>
        /// <returns>An IQueryable representing all entities</returns>
        IQueryable<T> GetAll(bool tracking = true);

        /// <summary>
        /// Asynchronously gets a single entity from the repository based on the specified criteria
        /// </summary>
        /// <param name="method">The criteria to filter the entity</param>
        /// <param name="tracking">Indicates whether change tracking is enabled (default is true)</param>
        /// <returns>A task representing the asynchronous operation and returning a single entity</returns>
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);

        /// <summary>
        /// Asynchronously gets a single entity from the repository based on its identifier
        /// </summary>
        /// <param name="id">The identifier of the entity to retrieve</param>
        /// <param name="tracking">Indicates whether change tracking is enabled (default is true)</param>
        /// <returns>A task representing the asynchronous operation and returning a single entity</returns>
        Task<T> GetByIdAsync(Guid id, bool tracking = true);
    }
}