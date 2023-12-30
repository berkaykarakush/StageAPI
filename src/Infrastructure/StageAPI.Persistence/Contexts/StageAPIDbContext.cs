using Microsoft.EntityFrameworkCore;
using StageAPI.Domain.Entities;

namespace StageAPI.Persistence.Contexts
{
    public class StageAPIDbContext : DbContext
    {
        public StageAPIDbContext(DbContextOptions<StageAPIDbContext> options) : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }
    }
}