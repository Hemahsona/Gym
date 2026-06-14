using Gym.DataAccess.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.ViewModels.Session
{
    public class SessionIndexViewModel
    {
        public int Id { get; set; }
        public string Speciality { get; set; }
        public string CategoryName { get; set; }
        public string TrainerName { get; set; }
        public string DisplayDate => $"{StartDate:MMM dd yyyy}";
        public string Description { get; set; } = null!;
        public string BookedCount { get; set; }
        public SessionStatus Status { get; set; }

        public int Capacity { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        //public TimeSpan Duration => EndDate - StartDate;
        public string Duration
        {
            get
            {
                TimeSpan duration = EndDate - StartDate;
                return $"{(int)duration.TotalHours} Hours {duration.Minutes} Minutes";
            }
        }
        public string TimeRangeDisplay => $"{StartDate: hh mm tt}"; 
        public string HeaderClass => Status switch
        {
            SessionStatus.Upcoming => "bg-primary",
            SessionStatus.Ongoing => "bg-success",
            SessionStatus.Compeleted => "bg-secondary",
        };
    }
}
