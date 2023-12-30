using StageAPI.Application.Repositories.Activity;

namespace StageAPI.Application.UnitOfWorks
{
    /// <summary>
    /// Represents the interface for unit of work. It implements the unit of work pattern, where database operations are consolidated and managed
    /// </summary>
    public interface IUnitOfWork : IAsyncDisposable
    {
        /// <summary>
        /// Represents the property for the repository managing activity write operations
        /// </summary>
        IActivityWriteRepository ActivityWriteRepository { get; }
        /// <summary>
        /// Represents the property for the repository managing activity read operations
        /// </summary>
        IActivityReadRepository ActivityReadRepository { get; }
        /// <summary>
        /// Performs the operation of persisting changes to the database, making them permanent
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A task representing the asynchronous operation, returning the count of changes saved to the database</returns>
        Task<int> SaveAsync();
    }
}