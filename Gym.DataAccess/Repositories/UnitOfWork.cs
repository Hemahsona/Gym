using Gym.DataAccess.Data.Context;
using Gym.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDBContext _dbContext;

        private IMemberRepository _members;
        private IBookingRepository _bookings;
        private IPlanRepository _plans;
        private IRepository<HealthRecord> _healthRecords;
        private IRepository<Category> _categories;

        public IMemberRepository Members => _members ??= new MemberRepository(_dbContext);
        public IBookingRepository Bookings => _bookings ??= new BookingRepository(_dbContext);

        public IPlanRepository Plans => _plans ??= new PlanRepository(_dbContext);
        public IRepository<HealthRecord> HealthRecords => _healthRecords ??= new Repository<HealthRecord>(_dbContext);
        public IRepository<Category> Categories => _categories ??= new Repository<Category>(_dbContext);



        public UnitOfWork(GymDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _dbContext.SaveChangesAsync(cancellationToken);

    }
}
