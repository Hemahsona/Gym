using Gym.DataAccess.Data.Context;
using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Gym.DataAccess.Repositories
{
    public class MemberShipRepository(GymDBContext dbContext) : Repository<MemberShip>(dbContext), IMemberShipRepository
    {


        public async Task<List<MemberShip>> HasPlanAndMemebrAsync(Expression<Func<MemberShip, object>>[]? includes = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<MemberShip> query =  dbContext.MemberShips.AsNoTracking();
            if(includes is not null)
            {
                foreach(var include in includes)
                    query = query.Include(include);
            }
            return await query.ToListAsync(cancellationToken);
        }
    }
}
