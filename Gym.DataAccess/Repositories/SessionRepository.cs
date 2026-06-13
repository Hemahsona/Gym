using Gym.DataAccess.Data.Context;
using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Gym.DataAccess.Repositories
{
    public class SessionRepository(GymDBContext dbContext) : Repository<Session>(dbContext), ISessionRepository
    {
        private readonly GymDBContext _dbContext = dbContext;

        public async Task<List<Session>> HasTrainerAsync(
            Expression<Func<Session, object>>[]? includes = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Session> query = _dbContext.Sessions.AsNoTracking();

            if (includes is not null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.ToListAsync(cancellationToken);
        }

    }
}
