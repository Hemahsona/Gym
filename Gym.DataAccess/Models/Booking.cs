using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Models
{
    public class Booking : BaseEntity
    {
        public DateTime Date { get; set; }
        public bool IsAttented { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public int SessionId { get; set; }
        public Session Session { get; set; }
        public DateTime BookedAt { get; set; }

    }
}
