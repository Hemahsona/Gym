using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Data.OwnedType
{

    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }

    }
}
