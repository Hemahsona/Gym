using Gym.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Repositories
{
    public interface IUnitOfWork 
    {
        IMemberRepository Members { get; }
        IBookingRepository Bookings { get; }
        IPlanRepository Plans { get; }
        IRepository<HealthRecord> HealthRecords { get; }
        IRepository<Category> Categories { get; }
        //IRepository<Session> Sessions { get; }
        ITrainerReposatiory Trainers { get; }
        ISessionRepository Sessions { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);


    }
}
