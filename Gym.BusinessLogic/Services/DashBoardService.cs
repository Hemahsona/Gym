using Gym.BusinessLogic.ViewModels.Home;
using Gym.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.Services
{
    public class DashBoardService(IUnitOfWork unitOfWork) : IDashBoardService
    {
        public async Task<Result<HomeDasgBoardViewModel>> GetHomePageAsync(CancellationToken ct = default)
        {
            return Result<HomeDasgBoardViewModel>.IsSuccess(new HomeDasgBoardViewModel
            {
                TotalMembers = await unitOfWork.Members.CountAsync(cancellationToken: ct),
                Trainers = await unitOfWork.Trainers.CountAsync(cancellationToken: ct),
                ActiveMember = await unitOfWork.Members.CountAsync(cancellationToken: ct),
                UpcomingSessions = await unitOfWork.Sessions.CountAsync(cancellationToken: ct),
                OngoingSessions = await unitOfWork.Sessions.CountAsync(cancellationToken: ct),
                CompletedSessions = await unitOfWork.Sessions.CountAsync(cancellationToken: ct),
            });
        }
    }
}
