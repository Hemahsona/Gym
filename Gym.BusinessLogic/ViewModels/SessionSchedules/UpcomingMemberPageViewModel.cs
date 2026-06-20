using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.ViewModels.SessionSchedules
{
    public class UpcomingMemberPageViewModel
    {
        public UpcomingSessionMemberSessionScheduleViewModel Session { get; set; }
        public List<UpcomingMemberSessionScheduleViewModel> Bookings { get; set; }
    }
}
