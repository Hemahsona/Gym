using Gym.DataAccess.Data.Context;
using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq.Expressions;
using System.Text;

namespace Gym.DataAccess.Repositories
{
    public class Repository<TEntity>(GymDBContext dbContext) : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly GymDBContext _dbContext = dbContext;
        private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
          => await _dbSet.AsNoTracking().ToListAsync(cancellationToken);


        public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => await _dbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        public async Task<TEntity> GetByIdIncludingDeletedAsync(int id, CancellationToken cancellationToken = default)
            => await _dbSet.IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => await _dbSet.AnyAsync(predicate, cancellationToken);
        public async Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => await _dbSet.Where(predicate).ToListAsync(cancellationToken);

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
            => await _dbContext.AddAsync(entity);

        public void Deleted(TEntity entity)
        {
            entity.IsDeleted = true;
            _dbContext.Update(entity);

        }

        public void Update(TEntity entity)
            => _dbContext.Update(entity);


        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _dbContext.SaveChangesAsync(cancellationToken);

    }
}
