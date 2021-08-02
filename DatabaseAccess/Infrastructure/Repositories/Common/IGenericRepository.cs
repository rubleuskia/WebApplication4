using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DatabaseAccess.Entities.Common;

namespace DatabaseAccess.Infrastructure.Repositories.Common
{
    public interface IGenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task Create(TEntity account);
        Task Update(TEntity account);
        Task Delete(Guid entityId);
        Task<TEntity> GetById(Guid entityId);
        Task<TEntity[]> GetAll();
        Task<TEntity[]> GetAll(Expression<Func<TEntity, bool>> filter);

        Task<TEntity[]> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity[]> GetWithInclude(Expression<Func<TEntity, bool>> predicate,
             params Expression<Func<TEntity, object>>[] includeProperties);
    }
}