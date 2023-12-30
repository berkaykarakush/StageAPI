using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace StageAPI.Application
{
    public class PagedList<T> : List<T>
    {
        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// Gets or sets the total number of pages.
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// Gets or sets the number of items per page.
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Gets or sets the total number of items.
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// /// Retrieves list items, total number of items, and page information.
        /// </summary>
        /// <param name="items">List items</param>
        /// <param name="count">Total number of items</param>
        /// <param name="pageNumber">Current page number</param>
        /// <param name="pageSize">Number of items per page</param>
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalCount = count;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }
        /// <summary>
        /// Asynchronously, the method that creates the paging structure from the IQueryable resource
        /// </summary>
        /// <param name="source">IQueryable resource to be paged</param>
        /// <param name="pageNumber">Current page number</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <returns>Created pagination structure</returns>
        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            // Asynchronously retrieves the total number of items from the source.
            var count = await source.CountAsync();
            // Applies paging and retrieves items with the specified page number at the specified page size.
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            // Create a new PagedList instance and return the paging structure.
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}