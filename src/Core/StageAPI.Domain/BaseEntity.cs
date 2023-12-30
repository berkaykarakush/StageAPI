namespace StageAPI.Domain
{
    /// <summary>
    /// Represents the base class for entities
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}