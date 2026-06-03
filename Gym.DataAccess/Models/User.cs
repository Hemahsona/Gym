using Gym.DataAccess.Data.Enums;
using Gym.DataAccess.Data.OwnedType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Address Address { get; set; }
    }
}
