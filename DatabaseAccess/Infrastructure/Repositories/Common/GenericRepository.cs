using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DatabaseAccess.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAccess.Infrastructure.Repositories.Common
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbSet<TEntity> _dbSet;

        protected GenericRepository(ApplicationContext context)
        {
            _dbSet = context.Set<TEntity>();
        }

        public async Task Create(TEntity account)
        {
            await _dbSet.AddAsync(account);
        }

        public Task Update(TEntity account)
        {
            _dbSet.Update(account);
            return Task.CompletedTask;
        }

        public async Task Delete(Guid entityId)
        {
            var entity = await GetById(entityId);
            _dbSet.Remove(entity);
        }

        public async Task<TEntity> GetById(Guid entityId)
        {
           var entity = await _dbSet.FindAsync(entityId).AsTask();
           if (entity == null)
           {
               throw new InvalidOperationException($"Entity with ID: {entityId} does not exist.");
           }
           return entity;
        }

        public Task<TEntity[]> GetAll()
        {
            return _dbSet.AsNoTracking().ToArrayAsync();
        }

        public Task<TEntity[]> GetAll(Expression<Func<TEntity, bool>> filterExpression)
        {
            return _dbSet.AsNoTracking().Where(filterExpression).ToArrayAsync();
        }

        public Task<TEntity[]> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Include(includeProperties).ToArrayAsync();
        }

        public Task<TEntity[]> GetWithInclude(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToArrayAsync();
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return includeProperties.Aggregate(_dbSet.AsNoTracking(), (current, property) => current.Include(property));
        }
    }
}