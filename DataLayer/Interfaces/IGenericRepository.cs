using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DataLayer.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(object id);

        Task<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate = null, bool WithTracking = false, bool IgnoreQueryFilters = false, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null, Expression<Func<TEntity, TEntity>>? select = null, bool asSplitQuery = false);

        Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);

        IList<TEntity> GetAll();

        Task Add(TEntity entity);

        Task<TKey> GetMaxColumn<TKey>(Expression<Func<TEntity, TKey>> Key, Expression<Func<TEntity, bool>> filter = null);

        Task<List<TKey>> GetDistinctColumn<TKey>(Expression<Func<TEntity, TKey>> Key);

        Task AddRange(IEnumerable<TEntity> entities);

        Task UpdateRange(IEnumerable<TEntity> entities);

        Task<TEntity> AddAndReturnEntity(TEntity entity);

        Task Delete(Expression<Func<TEntity, bool>> predicate);

        Task Delete(TEntity entity);

        Task DeleteRange(IEnumerable<TEntity> entities);

        Task Edit(TEntity entity);

        Task<List<TEntity>> GetAll<TKey>(int blockSize, int blockNumber, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy);

        Task<IList<TEntity>> List(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null, Expression<Func<TEntity, TEntity>>? select = null);

        Task<(IList<TEntity> EntityData, int Count)> ListWithPaging(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? page = null, int? pageSize = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null, Expression<Func<TEntity, TEntity>>? select = null);

        bool Any(Expression<Func<TEntity, bool>> filter = null);

        Task<TTarget> Mapping<TSource, TTarget>(TSource obj);
    }
}
