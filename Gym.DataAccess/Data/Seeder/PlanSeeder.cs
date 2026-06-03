using Gym.DataAccess.Data.Context;
using Gym.DataAccess.Models;

using Microsoft.EntityFrameworkCore;

namespace Gym.DataAccess.Data.Seeder
{
    public static class PlanSeeder
    {
        public static async Task SeedAsync(GymDBContext dbContext)
        {
           

            if (await dbContext.Plans.AnyAsync())
                return;

            var plans = new List<Plan>
        {
            new Plan
            {
                Name = "Basic",
                Description = "Access to gym equipment only",
                DurationDays = 30,
                Price = 300,
                IsActive = false,
            },

            new Plan
            {
                Name = "Silver",
                Description = "Gym + cardio classes",
                DurationDays = 90,
                Price = 800,
                IsActive = false
            },

            new Plan
            {
                Name = "Gold",
                Description = "Full access with personal trainer",
                DurationDays = 180,
                Price = 1500,
                IsActive = true
            },

            new Plan
            {
                Name = "Premium",
                Description = "VIP membership with all services",
                DurationDays = 365,
                Price = 2500,
                IsActive = true,
            }
        };

            await dbContext.Plans.AddRangeAsync(plans);

            await dbContext.SaveChangesAsync();
        }
    }
}
