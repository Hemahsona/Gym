using Gym.DataAccess.Data.Context;
using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Repositories
{
    public class PlanRepository(GymDBContext dbContext) : Repository<Plan>(dbContext), IPlanRepository
    {
        //private readonly GymDBContext _dbContext;

        //public PlanRepository(GymDBContext dbContext) : base(dbContext)
        //{
        //    _dbContext = dbContext;
        //}
        public async Task Add(Plan plan) => await dbContext.Plans.AddAsync(plan);


        public async void Delete(Plan plan) => dbContext.Plans.Remove(plan);


        public async Task<IEnumerable<Plan>> GetAllAsync() => await dbContext.Plans.ToListAsync();


        public async Task<Plan?> GetByIdAsync(int id) => await dbContext.Plans.FirstOrDefaultAsync(p => p.Id == id);


        public void Update(Plan plan) => dbContext.Plans.Update(plan);


        public async Task<int> SaveChanges() => await dbContext.SaveChangesAsync();
    }
}
