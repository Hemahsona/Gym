using Gym.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Gym.DataAccess.Repositories
{
    public interface IMemberShipRepository : IRepository<MemberShip>
    {
        Task<List<MemberShip>> HasPlanAndMemebrAsync(Expression<Func<MemberShip, object>>[]? includes = default, CancellationToken cancellationToken = default);
 

    }
}
