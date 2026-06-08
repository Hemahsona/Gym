using Gym.DataAccess.Data.Context;
using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Repositories
{
    public class MemberRepository(GymDBContext dbContext) : Repository<Member>(dbContext), IMemberRepository
    {
        private readonly GymDBContext _dbContext = dbContext;

        public async Task<bool> HasUpcomingBookingAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Booking>()
                .AnyAsync(b => b.MemberId == id && b.Session.EndDate >= DateTime.Now, cancellationToken);
        }

        public async Task<bool> IsEmailExists(string normalizeEmail, int? excludedId = null, CancellationToken cancellationToken = default)
            => await _dbContext.Set<Member>().AnyAsync(m => m.Email == normalizeEmail && (!excludedId.HasValue || m.Id != excludedId.Value), cancellationToken);
        

        public Task<bool> IsPhoneExists(string normalizePhone, int? excludedId = null, CancellationToken cancellationToken = default)
            => _dbContext.Set<Member>().AnyAsync(m => m.Phone == normalizePhone && (!excludedId.HasValue || m.Id != excludedId.Value), cancellationToken);

    }
}
