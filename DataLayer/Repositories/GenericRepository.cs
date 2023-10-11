using DataLayer.Interfaces;
using DataLayer.Shared;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DbContext;

namespace DataLayer.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly MainDbContext context;
        private readonly DbSet<TEntity> dbSet;

        public GenericRepository(IEfContextFactory efContextFactory)
        {
            this.context = efContextFactory.Create();
            this.dbSet = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetById(object id)
        {
            if (id is null)
            {
                return null;
            }
            TEntity item = await dbSet.FindAsync(id);
            if (item == null)
            {
                return null;
            }

            context.Entry(item).State = EntityState.Detached;
            return item;
        }

        public async Task<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate = null, bool WithTracking = false,
            bool IgnoreQueryFilters = false, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null,
            Expression<Func<TEntity, TEntity>>? select = null, bool asSplitQuery = false)
        {
            IQueryable<TEntity> query = dbSet;

            query = !WithTracking ? query.AsNoTracking() : query.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includeProperties != null)
            {
                query = includeProperties(query);
            }

            if (select != null)
            {
                query = query.Select(select);
            }

            if (IgnoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (asSplitQuery)
            {
                query = query.AsSplitQuery();
            }

            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = dbSet.AsQueryable();

            try
            {
                if (predicate != null)
                {
                    return await dbSet.Where(predicate).ToListAsync();
                }
                else return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<TEntity>();
            }

        }

        public virtual IList<TEntity> GetAll()
        {
            IQueryable<TEntity> query = dbSet.AsQueryable();
            List<TEntity> list = query.ToList();
            return list;
        }

        public virtual async Task Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entry = dbSet.Add(entity);

            _ = await context.SaveChangesAsync();
        }

        public virtual async Task<TKey> GetMaxColumn<TKey>(Expression<Func<TEntity, TKey>> Key, Expression<Func<TEntity, bool>> filter = null)
        {

            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            int count = query.Count();
            TKey maxColumn = count == 0 ? default : await query.MaxAsync(Key);
            return maxColumn;
        }

        public virtual async Task<List<TKey>> GetDistinctColumn<TKey>(Expression<Func<TEntity, TKey>> Key)
        {

            IQueryable<TEntity> query = dbSet;
            IQueryable<TKey> columnList = query.Select(Key).Distinct();
            return await columnList.ToListAsync();
        }

        public virtual async Task AddRange(IEnumerable<TEntity> entities)
        {

            await dbSet.AddRangeAsync(entities);
            _ = await context.SaveChangesAsync();
        }

        public virtual async Task UpdateRange(IEnumerable<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
            _ = await context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> AddAndReturnEntity(TEntity entity)
        {

            _ = await dbSet.AddAsync(entity);
            _ = await context.SaveChangesAsync();
            return entity;

        }

        public virtual async Task Delete(Expression<Func<TEntity, bool>> predicate)
        {

            dbSet.RemoveRange(dbSet.Where(predicate));
            _ = await context.SaveChangesAsync();
        }

        public virtual async Task Delete(TEntity entity)
        {
            bool isDetached = context.Entry(entity).State == EntityState.Detached;
            if (isDetached)
            {
                context.Entry(entity).State = EntityState.Deleted;
                _ = dbSet.Attach(entity);
            }
            var entry = dbSet.Remove(entity);

            _ = await context.SaveChangesAsync();
        }

        public virtual async Task DeleteRange(IEnumerable<TEntity> entities)
        {

            foreach (TEntity item in entities)
            {
                bool isDetached = context.Entry(item).State == EntityState.Detached;
                if (isDetached)
                {
                    context.Entry(item).State = EntityState.Deleted;
                    _ = dbSet.Attach(item);
                }
            }
            dbSet.RemoveRange(entities);
            _ = await context.SaveChangesAsync();
        }

        public virtual async Task Edit(TEntity entity)
        {
            var entry = dbSet.Update(entity);
            context.Entry(entity).State = EntityState.Modified;

            _ = await context.SaveChangesAsync();
            context.Entry(entity).State = EntityState.Detached;
        }

        public virtual async Task<List<TEntity>> GetAll<TKey>(int blockSize, int blockNumber, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy)
        {


            int startIndex = (blockNumber - 1) * blockSize;
            List<TEntity> list = await dbSet.Where(predicate)
                .OrderBy(orderBy).Skip(startIndex).Take(blockSize).ToListAsync();
            return list;
        }

        public async Task<IList<TEntity>> List(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null, Expression<Func<TEntity, TEntity>>? select = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (includeProperties != null)
            {
                query= includeProperties(query);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy.Invoke(query);
            }

            if (select != null)
            {
                query = query.Select(select);
            }

            List<TEntity> list = await query.ToListAsync();
            return list;
        }

        public async Task<(IList<TEntity> EntityData, int Count)> ListWithPaging(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? page = null, int? pageSize = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null, Expression<Func<TEntity, TEntity>>? select = null)
        {

            IQueryable<TEntity> query = dbSet;

            if (includeProperties != null)
            {
                query = includeProperties(query);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy.Invoke(query);
            }

            if (select != null)
            {
                query = query.Select(select);
            }

            int count = query.Count();

            if (page != null && pageSize != null)
            {
                query = query
                    .Skip(page.Value)
                    .Take(pageSize.Value);
            }

            List<TEntity> data = await query.ToListAsync();
            return (data, count);
        }

        public bool Any(Expression<Func<TEntity, bool>> filter = null)
        {

            IQueryable<TEntity> query = dbSet;
            return filter != null ? query.Any(filter) : query.Any();
        }

        #region Mapping
        public async Task<TTarget> Mapping<TSource, TTarget>(TSource obj)
        {
            try


            {


                var target = typeof(TTarget);
                var newTargetObj = Activator.CreateInstance(target);

                var source = typeof(TSource);
                var sourceProps = source.GetProperties();

                foreach (var sourceProp in sourceProps)
                {
                    var targetProp = target.GetProperty(sourceProp.Name);
                    var sourceValue = sourceProp.GetValue(obj);

                    if (targetProp != null && targetProp.CanWrite)
                    {
                        if (sourceValue != null)
                        {
                            if (sourceProp.PropertyType == targetProp.PropertyType)
                            {
                                targetProp.SetValue(newTargetObj, sourceValue);
                            }
                            else if (sourceProp.PropertyType.IsClass && !sourceProp.PropertyType.IsPrimitive)
                            {
                                var nestedTargetObj = Mapping<object, object>(sourceValue);
                                targetProp.SetValue(newTargetObj, nestedTargetObj);
                            }
                        }
                    }
                }

                return (TTarget)newTargetObj;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }
        }
        #endregion
    }

}
