using Gym.DataAccess.Data.Context;

namespace Gym.DataAccess.Data.Seeder
{
    public static class DataBAseSeeder
    {
        public static async Task SeedAllAsync(GymDBContext dbContext)
        {
            await PlanSeeder.SeedAsync(dbContext);
            await CategorySeeder.SeedAsync(dbContext);
        }
    }
}
