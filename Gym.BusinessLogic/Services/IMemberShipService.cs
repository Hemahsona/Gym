using Gym.BusinessLogic.ViewModels.MebmerShip;
using Gym.BusinessLogic.ViewModels.Member;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.Services
{
    public interface IMemberShipService
    {
        Task<Result<IEnumerable<IndexMemberShipViewModel>>> GetAllAsync( CancellationToken ct);
        Task<Result<IndexMemberShipViewModel>> GetById(int id, CancellationToken ct);
        Task<Result> CreateAsync(CreateMemberShipViewModel model,CancellationToken ct);
        Task<Result<IReadOnlyList<MemberLockupItem>>> GetMemberLockupAsync(CancellationToken ct);
        Task<Result<IReadOnlyList<PlanLockupItem>>> GetPlanLockupAsync(CancellationToken ct);
        Task<Result> DeleteAsync(int id, CancellationToken ct);

    }
}
