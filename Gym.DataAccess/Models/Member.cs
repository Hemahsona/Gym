using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Models
{
    public class Member :User
    {
        public string Photo { get; set; }
        public DateOnly JoinDate { get; set; }
        public HealthRecord HealthRecord { get; set; }
        public ICollection<MemberShip> MemberShips { get; set; }
        public ICollection<Booking> Bookings { get; set; } = [];
    }
}
