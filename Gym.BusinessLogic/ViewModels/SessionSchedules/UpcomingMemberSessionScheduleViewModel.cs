using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.ViewModels.SessionSchedules
{
    public class UpcomingMemberSessionScheduleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }

        public string DisplayDate => $"{StartDate:MMM dd yyyy}";
    }
}
