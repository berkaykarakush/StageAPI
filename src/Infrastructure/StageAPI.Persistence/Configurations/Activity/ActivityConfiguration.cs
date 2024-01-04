using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StageAPI.Persistence.Configurations.Activity
{
    /// <summary>
    /// This class provides configurations used by Entity Framework Core for the Activity type
    /// </summary>
    public class ActivityConfiguration : IEntityTypeConfiguration<Domain.Entities.Activity>
    {
        /// <summary>
        /// Configures the table and column settings in the database for the Activity type.
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Domain.Entities.Activity> builder)
        {
            // Specifies the primary key for the Activity table.
            builder.HasKey(a => a.Id);
            // Specifies that the Title field cannot be empty (IsRequired).
            builder.Property(a => a.Title).IsRequired();
            // Specifies that the Description field cannot be empty.
            builder.Property(a => a.Description).IsRequired();
            // Specifies that the Category field cannot be empty.
            builder.Property(a => a.Category).IsRequired();
            // Specifies that the City field cannot be empty.
            builder.Property(a => a.City).IsRequired();
            // Specifies that the Venue field cannot be empty.
            builder.Property(a => a.Venue).IsRequired();
        }
    }
}