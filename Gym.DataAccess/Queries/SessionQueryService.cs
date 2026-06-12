using Gym.DataAccess.Data.Context;
using Gym.DataAccess.Queries.Dtos;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Queries
{
    public class SessionQueryService(GymDBContext dbContext) : ISessionQueryService
    {
        public async Task<IReadOnlyList< SessionIndexViewModelQueryService>> GetIndexItemsAsync(CancellationToken ct = default)
        {
            return await dbContext.Sessions
                .Select( s => new SessionIndexViewModelQueryService
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
                }).ToListAsync();

        }
    }
}
