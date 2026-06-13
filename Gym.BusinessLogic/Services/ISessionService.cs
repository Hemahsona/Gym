using Gym.BusinessLogic.ViewModels.Session;

using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.Services
{
    public interface ISessionService
    {
        Task<IReadOnlyList<SessionIndexViewModel>> GetIndexItemsAsync(CancellationToken ct = default);
        Task<Result<SessionDetailsViewModel>> GetDetailsAsync(int id, CancellationToken ct = default);
        Task<Result<IReadOnlyList<CategoryLockupItem>>> GetGategoryAsync( CancellationToken ct = default);
        Task<Result<IReadOnlyList<TrainerLockupItem>>> GetTrainersAsync(CancellationToken ct = default);
        Task<Result> CreateAsync(SessionCreateViewModel model, CancellationToken ct = default);

    }
}
