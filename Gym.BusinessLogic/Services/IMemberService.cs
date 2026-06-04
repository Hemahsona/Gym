using Gym.BusinessLogic.ViewModels.Member;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.Services
{
    public interface IMemberService
    {
        public Task<IEnumerable<MemberIndexViewModel>> GetAllAsync(CancellationToken ct);
        public Task<bool> CreateAsync(CreateMemberViewModel model, CancellationToken ct);
    }
}
