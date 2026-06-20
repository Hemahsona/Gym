using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gym.BusinessLogic.ViewModels.SessionSchedules
{
    public class CreateSessionScheduleViewModel
    {
        [Required(ErrorMessage = "Name Is Required")]
        [Display(Name = "Member")]
        public int MemberId { get; set; } = default!;
        public int SessionId { get; set; }
        public DateTime StartDate { get; set; }
        public IReadOnlyList<MemberLockupItem> Members { get; set; } = default!;
        public int Phone { get; set; }

    }
    public sealed class MemberLockupItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
