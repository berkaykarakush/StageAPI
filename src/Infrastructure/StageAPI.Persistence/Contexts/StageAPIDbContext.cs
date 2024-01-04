using Microsoft.EntityFrameworkCore;
using StageAPI.Domain;
using StageAPI.Domain.Entities;
using StageAPI.Persistence.Configurations.Activity;

namespace StageAPI.Persistence.Contexts
{
    public class StageAPIDbContext : DbContext
    {
        public StageAPIDbContext(DbContextOptions<StageAPIDbContext> options) : base(options)
        {
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
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedData = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}