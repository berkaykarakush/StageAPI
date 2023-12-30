using Microsoft.EntityFrameworkCore;
using StageAPI.Domain.Entities;
using StageAPI.Persistence.Contexts;

namespace StageAPI.Persistence
{
    public class Seed
    {
        /// <summary>
        ///  Seed the database with initial data if no activities exist.
        /// </summary>
        /// <param name="context">The database context</param>
        /// <returns></returns>
        public static async Task SeedData(StageAPIDbContext context)
        {
            // Check if there are no activities in the database
            if (!context.Activities.Any())
            {
                // Create a list of sample activities
                var activities = new List<Activity>
                {
                    new Activity
                    {
                        Title = "Past Activity 1",
                        Description = "Activity 2 months ago",
                        Category = "drinks",
                        City = "London",
                        Venue = "Pub"
                    },
                    new Activity
                    {
                        Title = "Past Activity 2",
                        Description = "Activity 1 months ago",
                        Category = "culture",
                        City = "Paris",
                        Venue = "Louvre"
                    },
                    new Activity
                    {
                        Title = "Future Activity 1",
                        Description = "Activity 1 month in future",
                        Category = "culture",
                        City = "London",
                        Venue = "Natural"
                    },
                    new Activity
                    {
                        Title = "Future Activity 2",
                        Description = "Activity 2 months in future",
                        Category = "music",
                        City = "London",
                        Venue = "02 Are"
                    },
                    new Activity
                    {
                        Title = "Future Activity 3",
                        Description = "Activity 3 months in future",
                        Category = "drinks",
                        City = "London",
                        Venue = "Another"
                    },
                    new Activity
                    {
                        Title = "Future Activity 4",
                        Description = "Activity 4 months in future",
                        Category = "drinks",
                        City = "London",
                        Venue = "Yet ano"
                    },
                    new Activity
                    {
                        Title = "Future Activity 5",
                        Description = "Activity 5 months in future",
                        Category = "drinks",
                        City = "London",
                        Venue = "Just an"
                    },
                    new Activity
                    {
                        Title = "Future Activity 6",
                        Description = "Activity 6 months in future",
                        Category = "music",
                        City = "London",
                        Venue = "RoundH"
                    },
                    new Activity
                    {
                        Title = "Future Activity 7",
                        Description = "Activity 2 months ago",
                        Category = "travel",
                        City = "London",
                        Venue = "Somew"
                    },
                    new Activity
                    {
                        Title = "Future Activity 8",
                        Description = "Activity 8 months in future",
                        Category = "film",
                        City = "London",
                        Venue = "Cinema"
                    },
                };
                // Add the activities to the DbSet
                await context.Activities.AddRangeAsync(activities);
                // Save the changes to the database
                await context.SaveChangesAsync();
            }
        }
    }
}