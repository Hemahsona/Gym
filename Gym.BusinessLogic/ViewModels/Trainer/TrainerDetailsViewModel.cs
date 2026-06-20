using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.ViewModels.Trainer
{
    public class TrainerDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Specialties { get; set; }

    }
}
