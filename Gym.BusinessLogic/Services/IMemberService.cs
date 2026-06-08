using Gym.BusinessLogic.ViewModels.HealthRecord;
using Gym.BusinessLogic.ViewModels.Member;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.Services
{
    public interface IMemberService
    {
        public Task<IEnumerable<MemberIndexViewModel>> GetAllAsync(CancellationToken ct);
        public Task<Result> CreateAsync(CreateMemberViewModel model, CancellationToken ct);
        Task<MemberDetailsViewModel> GetDetailsAsync(int id,CancellationToken ct);
        Task<HealthRecordDetailsModelView> GetHealthRecordDetailsAsync(int id,CancellationToken ct);
        Task<EditMemberViewModel> GetForEditAsync(int id, CancellationToken ct);
        Task<Result> EditAsync(int id,EditMemberViewModel model, CancellationToken ct);
        Task<Result> DeleteAsync(int id, CancellationToken ct);
    }
}
