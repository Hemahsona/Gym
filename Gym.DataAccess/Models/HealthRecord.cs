using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Models
{
    public class HealthRecord : BaseEntity
    {
        public string Height { get; set; }
        public string Weight { get; set; }
        public string BloodType { get; set; }
        public string? Note { get; set; }
        public DateTime LastUpdate { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
    }
}
