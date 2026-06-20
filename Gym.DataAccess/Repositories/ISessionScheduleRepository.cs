using Gym.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Gym.DataAccess.Repositories
{
    public interface ISessionScheduleRepository : IRepository<Booking>
    {
        Task<List<Booking>> HasSessionAndMemebrAsync(Expression<Func<Booking, object>>[]? includes = default, CancellationToken cancellationToken = default);
        Task<List<Booking>> GetById(int id,bool trackerchange, Expression<Func<Booking, object>>[]? includes = default, CancellationToken cancellationToken = default);

    }
}
