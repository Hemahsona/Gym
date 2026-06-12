using Gym.DataAccess.Queries.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Queries
{
    public interface ISessionQueryService
    {
        Task<IReadOnlyList<SessionIndexViewModelQueryService>> GetIndexItemsAsync(CancellationToken ct = default);

    }
}
