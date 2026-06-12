using Gym.BusinessLogic.ViewModels.Session;
using Gym.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.Services
{
    public class SessionService(IUnitOfWork unitOfWork) : ISessionService
    {


        public async Task<IReadOnlyList<SessionIndexViewModel>> GetIndexItemsAsync(CancellationToken ct = default)
        {
            var sessions = await unitOfWork.Sessions.GetAllAsync(ct);
            var result = sessions.Select(s => new SessionIndexViewModel
            {
                Id = s.Id,
                TrainerName = s.Trainer.Name,
                Speciality = s.Category.Name,
                Description = s.Description,
                BookedCount = s.Bookings.Count.ToString(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Capacity = s.Capacity,
                CategoryName = s.Category.Name,
            }).ToList();
            return result;
        }
    }
}
