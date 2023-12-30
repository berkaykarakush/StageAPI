using Microsoft.EntityFrameworkCore;
using StageAPI.Domain;

namespace StageAPI.Application.Repositories
{
    /// <summary>
    /// Generic repository interface for database operations.
    /// </summary>
    /// <typeparam name="T">The type of entity that the repository operates on</typeparam>
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Gets the DbSet for the specified entity type.
        /// </summary>
        DbSet<T> Table {get;}
    }
}