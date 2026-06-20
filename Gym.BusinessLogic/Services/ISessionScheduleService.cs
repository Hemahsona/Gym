using Gym.BusinessLogic.ViewModels.Session;
using Gym.BusinessLogic.ViewModels.SessionSchedule;
using Gym.BusinessLogic.ViewModels.SessionSchedules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.Services
{
    public interface ISessionScheduleService
    {
        Task<Result<IEnumerable<IndexSessionScheduleViewModel>>> GetAllAsync(CancellationToken ct);
        Task<Result<IEnumerable<UpcomingMemberSessionScheduleViewModel>>> GetBookingsBySeesionId(int id, CancellationToken ct);
        Task<Result<UpcomingMemberSessionScheduleViewModel>> GetBookingByIdAsync(int id, CancellationToken ct);
        Task<Result<UpcomingSessionMemberSessionScheduleViewModel>> GetSessionById(int id, CancellationToken ct);
        Task<Result> CreateAsync(CreateSessionScheduleViewModel model, CancellationToken ct = default);
        Task<Result<IReadOnlyList<MemberLockupItem>>> GetMemberAsync(CancellationToken ct = default);
        Task<Result<int>> DeleteAsync(int id, CancellationToken ct);
        Task<Result<IEnumerable<OngoingMemberSessionScheduleViewModel>>> GetOngoingByBookingId(int id, CancellationToken ct = default);
        Task<Result> ToggleAttendanceAsync(int id, OngoingMemberSessionScheduleViewModel model, CancellationToken ct = default);


    }
}
