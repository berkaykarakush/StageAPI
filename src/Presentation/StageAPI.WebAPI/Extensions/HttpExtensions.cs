using System.Text.Json;

namespace StageAPI.WebAPI.Extensions
{
    /// <summary>
    /// Extension methods for handling HTTP-related operations.
    /// </summary>
    public static class HttpExtensions
    {
        /// <summary>
        /// Adds pagination information to the HTTP response headers.
        /// </summary>
        /// <param name="response">The HTTP response object.</param>
        /// <param name="currentPage">The current page number.</param>
        /// <param name="itemsPerPage">The number of items per page.</param>
        /// <param name="totalItems">The total number of items.</param>
        /// <param name="totalPages">The total number of pages.</param>
        public static void AddPaginationHeader(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            // Create an anonymous object representing the pagination information.
            var paginationHeader = new { currentPage, itemsPerPage, totalItems, totalPages };
            // Serialize the pagination information to JSON and add it to the response headers.
            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader));
            // Ensure that the "Pagination" header is exposed to the client in CORS scenarios.
            response.Headers.Add("Access-Control-Expose-Header", "Pagination");
        }
    }
}