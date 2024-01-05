using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StageAPI.Domain;
using StageAPI.Domain.Entities;
using StageAPI.Persistence.Configurations.Activity;

namespace StageAPI.Persistence.Contexts
{
    public class StageAPIDbContext : DbContext
    {
        public StageAPIDbContext(DbContextOptions<StageAPIDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        /// <summary>
        /// Gets or sets the DbSet for the Activity entity
        /// </summary>
        public DbSet<Activity> Activities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ActivityConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                if (data.State == EntityState.Added)
                {
                    data.Entity.CreatedData = DateTime.UtcNow;
                }
                else if (data.State == EntityState.Modified)
                {
                    data.Entity.UpdatedDate = DateTime.UtcNow;
                }
            }

            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving changes to the database", ex);
            }
        }

    }
}