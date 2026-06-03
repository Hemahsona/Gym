using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Models
{
    public class Category :BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Session> Sessions { get; set; }

    }
}
