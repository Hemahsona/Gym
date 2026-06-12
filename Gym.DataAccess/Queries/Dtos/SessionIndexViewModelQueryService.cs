using Gym.DataAccess.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Queries.Dtos
{
    public class SessionIndexViewModelQueryService
    {
        public int Id { get; set; }
        public string Speciality { get; set; }
        public string CategoryName { get; set; }

        public string TrainerName { get; set; }
        public int MyProperty { get; set; }
        public string Description { get; set; } = null!;
        public string BookedCount { get; set; }
        public SessionStatus Status { get; set; }

        public int Capacity { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public TimeSpan Duration => EndDate - StartDate;
        public string HeaderClass => Status switch
        {
            SessionStatus.Upcoming => "bg-primary",
            SessionStatus.Ongoing => "bg-success",
            SessionStatus.Compeleted => "bg-secondary",
        };
    }
}
