using Gym.DataAccess.Data.Context;
using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Gym.DataAccess.Repositories
{
    public class SessionScheduleRepository(GymDBContext dbContext) : Repository<Booking>(dbContext), ISessionScheduleRepository
    {
        public async Task<List<Booking>> GetById(int id, bool trackerchange, Expression<Func<Booking, object>>[]? includes = null, CancellationToken cancellationToken = default)
        {
            {
                IQueryable<Booking> query = dbContext.Bookings
                    .AsNoTracking();
                if (includes is not null)
                {
                    foreach (var include in includes)
                        query = query.Include(include);
                }
                return await query.ToListAsync(cancellationToken);
            }
        }

        public async Task<List<Booking>> HasSessionAndMemebrAsync(Expression<Func<Booking, object>>[]? includes = null, CancellationToken cancellationToken = default)
        {
            {
                IQueryable<Booking> query = dbContext.Bookings
                    .AsNoTracking();
                if (includes is not null)
                {
                    foreach (var include in includes)
                        query = query.Include(include);
                }
                return await query.ToListAsync(cancellationToken);
            }
        }


    }
}
