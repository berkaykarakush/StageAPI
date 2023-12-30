using StageAPI.Domain;

namespace StageAPI.Application.Repositories
{
    /// <summary>
    /// Generic repository interface for write operations (add, remove, save) on entities.
    /// </summary>
    /// <typeparam name="T">The type of entity that the repository operates on</typeparam>
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Asynchronously adds a single entity to the repository
        /// </summary>
        /// <param name="model">The entity to add</param>
        /// <returns>A task representing the asynchronous operation and returning a boolean indicating success</returns>
        Task<bool> AddAsync(T model);

        /// <summary>
        /// Asynchronously adds a list of entities to the repository
        /// </summary>
        /// <param name="datas">The list of entities to add</param>
        /// <returns>A task representing the asynchronous operation and returning a boolean indicating success</returns>
        Task<bool> AddRangeAsync(List<T> datas);

        /// <summary>
        /// Removes a single entity from the repository
        /// </summary>
        /// <param name="model">The entity to remove</param>
        /// <returns>A boolean indicating success</returns>
        bool Remove(T model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(Guid id);

        /// <summary>
        /// Asynchronously removes an entity from the repository based on its identifier
        /// </summary>
        /// <param name="datas">The identifier of the entity to remove</param>
        /// <returns>A task representing the asynchronous operation and returning a boolean indicating success</returns>
        bool RemoveRange(List<T> datas);

        /// <summary>
        /// Asynchronously saves changes made in the repository
        /// </summary>
        /// <returns>A task representing the asynchronous operation and returning the number of changes saved</returns>
        Task<int> SaveAsync();
    }
}