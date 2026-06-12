using Gym.BusinessLogic.ViewModels.Session;

using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.Services
{
    public interface ISessionService
    {
        Task<IReadOnlyList<SessionIndexViewModel>> GetIndexItemsAsync(CancellationToken ct = default);

    }
}
