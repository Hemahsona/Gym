using Gym.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Gym.DataAccess.Repositories
{
    public interface ISessionRepository : IRepository<Session>
    {
        Task<List<Session>> HasTrainerAsync(Expression<Func<Session, object>>[]? includes = default, CancellationToken cancellationToken = default);

    }
}
