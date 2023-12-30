namespace StageAPI.Application.RequestParameters
{
    /// <summary>
    /// Represents parameters for pagination
    /// </summary>
    public class PagingParams
    {
        /// <summary>
        /// The maximum number of items allowed per page
        /// </summary>
        private const int MaxPageSize = 50;
        /// <summary>
        /// Gets or sets the page number. Default is 1
        /// </summary>
        public int pageNumber { get; set; } = 1;
        /// <summary>
        /// The private field for page size, with a default value of 10
        /// </summary>
        private int _pageSize = 10;
        /// <summary>
        /// Gets or sets the page size, ensuring it does not exceed the maximum allowed
        /// </summary>
        public int pageSize   
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}