using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.ViewModels.MebmerShip
{
    public class IndexMemberShipViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PlanName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public string Status => EndDate < DateTime.Now ? "Expired": "Active" ;
    }
}
