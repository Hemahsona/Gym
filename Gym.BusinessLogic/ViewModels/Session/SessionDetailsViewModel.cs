using Gym.DataAccess.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.ViewModels.Session
{
    public class SessionDetailsViewModel
    {
        public string CategoryName { get; set; }
        public SessionStatus Status { get; set; }
        public string HeaderClass { get; set; }
        public string TrainerName { get; set; }
        public string Description { get; set; }
        public int BookedCount { get; set; }
        public int MaxCapacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Capacity => $"{BookedCount} / {MaxCapacity} spots";
        public string Duration
        {
            get
            {
                TimeSpan duration = EndDate - StartDate;
                return $"{(int)duration.TotalHours} Hours {duration.Minutes} Minutes";
            }
        }
    }
}
