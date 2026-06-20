using Gym.DataAccess.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.ViewModels.SessionSchedule
{
    public class IndexSessionScheduleViewModel
    {
        public int Id { get; set; }
        public string MemberName { get; set; }
        public string TrainerName { get; set; }
        public string PlanName { get; set; }
        //public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public string DisplayStartDate => $"{StartDate:hh:mm tt}";
        public DateTime EndDate { get; set; }
        public string DisplayEndDate => $"{EndDate:hh:mm tt}";
        public string CategoryName { get; set; }
        public string DisplayDate => $"{StartDate:MMM dd yyyy}";
        public string Description { get; set; }
        public SessionStatus Status { get; set; }
        public int Capacity { get; set; }
        public string BookedCount { get; set; }
        public string Duration
        {
            get
            {
                TimeSpan duration = EndDate - StartDate;
                return $"{(int)duration.TotalHours} Hours {duration.Minutes} Minutes";
            }
        }


        public string Speciality { get; set; }

    }
}
