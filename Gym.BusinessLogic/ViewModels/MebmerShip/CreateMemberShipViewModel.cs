using Gym.DataAccess.Models;
using System;
using System.Collections.Generic;
using Gym.DataAccess.Models;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gym.BusinessLogic.ViewModels.MebmerShip
{
    public class CreateMemberShipViewModel
    {
        [Required(ErrorMessage = "Name Is Required")]
        [Display(Name = "Member")]
        public int MemberId { get; set; } = default!;

        [Required(ErrorMessage = "Plan Is Required")]
        [Display(Name = "Plan")]
        public int PlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public IReadOnlyList<MemberLockupItem> Members { get; set; } = default!;
        public IReadOnlyList<PlanLockupItem> Plans { get; set; } = default!;

    }

    public sealed class MemberLockupItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
    }
    public sealed class PlanLockupItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
