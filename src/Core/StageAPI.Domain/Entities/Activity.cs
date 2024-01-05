namespace StageAPI.Domain.Entities
{
    /// <summary>
    /// Represents a activity
    /// </summary>
    public class Activity : BaseEntity
    {
        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Gets or state the date and time of instance creation
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Gets or sets the categry name
        /// </summary>
        public string? Category { get; set; }
        /// <summary>
        /// Gets or sets the city 
        /// </summary>
        public string? City { get; set; }
        /// <summary>
        /// Gets or sets the venue
        /// </summary>
        public string? Venue { get; set; }
    }
}