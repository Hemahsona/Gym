using Gym.BusinessLogic.ViewModels.HealthRecord;
using Gym.DataAccess.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gym.BusinessLogic.ViewModels.Trainer
{
    public class TrainerIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
        public DateOnly JoinDate { get; set; }
        public string Gender { get; set; }
        public string Specialties { get; set; }

    }
    }
