using Gym.DataAccess.Data.Context;
using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Repositories 
{
    public class TrainerRepository(GymDBContext dbContext) : Repository<Trainer>(dbContext), ITrainerReposatiory
    {
        private readonly GymDBContext _dbContext = dbContext;
        public async Task<bool> IsEmailExists(string normalizeEmail, int? excludedId = null, CancellationToken cancellationToken = default)
            => await _dbContext.Set<Trainer>().AnyAsync(m => m.Email == normalizeEmail && (!excludedId.HasValue || m.Id != excludedId.Value), cancellationToken);

        public Task<bool> IsPhoneExists(string normalizePhone, int? excludedId = null, CancellationToken cancellationToken = default)
            => _dbContext.Set<Trainer>().AnyAsync(m => m.Phone == normalizePhone && (!excludedId.HasValue || m.Id != excludedId.Value), cancellationToken);
    }
}
