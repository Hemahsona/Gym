using Gym.DataAccess.Data.Context;
using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Gym.DataAccess.Data.Seeder
{
    public static class CategorySeeder
    {
        public static async Task SeedAsync(GymDBContext dbContext)
        {
            if (await dbContext.Categories.AnyAsync())
                return;
            var Categories = new List<Category>
            {
                new Category { Name = "Protein" },
                new Category { Name = "Supplements" },
                new Category {  Name = "Equipment" }
            };

            await dbContext.Categories.AddRangeAsync(Categories);

            await dbContext.SaveChangesAsync();
        }


    }
}
