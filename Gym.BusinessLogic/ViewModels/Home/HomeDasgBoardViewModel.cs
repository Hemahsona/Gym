using Gym.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.ViewModels.Home
{
    public class HomeDasgBoardViewModel
    {
        public int TotalMembers { get; set; } = default!;
        public int ActiveMember { get; set; } = default!;
        public int Trainers { get; set; } = default!;
        public int UpcomingSessions { get; set; } = default!;
        public int OngoingSessions { get; set; } = default!;
        public int CompletedSessions { get; set; } = default!;
    }
}
