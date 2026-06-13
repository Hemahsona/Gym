using Gym.BusinessLogic.ViewModels.Session;
using Gym.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.Mappings
{
    public static class SessionMappingExtention
    {
        public static SessionIndexViewModel ToSessionIndexViewModel(this Session session)
        {
            return new SessionIndexViewModel
            {
                Id = session.Id,
                TrainerName = session.Trainer.Name,
                Description = session.Description,
                CategoryName = session.Category.Name,
                BookedCount = session.Bookings.Count.ToString(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Capacity = session.Capacity,
                Speciality = session.Category.Name,
            };
        }
    }
}