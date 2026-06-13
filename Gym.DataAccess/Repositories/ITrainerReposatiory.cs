using Gym.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Repositories
{
    public interface ITrainerReposatiory : IRepository<Trainer>
    {
        Task<bool> IsEmailExists(string normalizeEmail, int? excludedId = null, CancellationToken cancellationToken = default);
        Task<bool> IsPhoneExists(string normalizePhone, int? excludedId = null, CancellationToken cancellationToken = default);
    }
}
