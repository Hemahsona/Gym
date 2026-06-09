using Gym.DataAccess.Data.Context;
using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Repositories
{
    public class BookingRepository(GymDBContext dbContext) : Repository<Booking>(dbContext), IBookingRepository
    {
        private readonly GymDBContext _dbContext = dbContext;

        public async Task<bool> HasUpcomingBookingAsync(int memberId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Booking>()
                .AnyAsync(b => b.MemberId == memberId && b.Session.EndDate >= DateTime.Now, cancellationToken);
        }
    }


}
