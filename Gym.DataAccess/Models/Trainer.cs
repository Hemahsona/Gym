using Gym.DataAccess.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Models
{
    public class Trainer : User
    {
        public Specialties Specialties { get; set; }
        public DateTime HireDate { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
