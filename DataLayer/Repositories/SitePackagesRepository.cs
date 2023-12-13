using DataLayer.DbContext;
using DataLayer.Entities;
using DataLayer.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class SitePackagesRepository
    {

        protected readonly MainDbContext context;
        private readonly DbSet<Package> dbSet;

        public SitePackagesRepository(IEfContextFactory efContextFactory)
        {
            this.context = efContextFactory.Create();
            this.dbSet = context.Set<Package>();
        }

        public virtual async Task<Package> GetById(object id)
        {
            if (id is null)
            {
                return null;
            }
            Package item = await dbSet.FindAsync(id);
            if (item == null)
            {
                return null;
            }

            context.Entry(item).State = EntityState.Detached;
            return item;
        }

        public virtual async Task Edit(Package entity)
        {
            var entry = dbSet.Update(entity);
            context.Entry(entity).State = EntityState.Modified;

            _ = await context.SaveChangesAsync();
            context.Entry(entity).State = EntityState.Detached;
        }

        public virtual async Task Add(Package entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entry = dbSet.Add(entity);

            _ = await context.SaveChangesAsync();
        }


        public async Task<(IList<Package> EntityData, int Count)> ListWithPaging(Expression<Func<Package, bool>> filter = null, Func<IQueryable<Package>, IOrderedQueryable<Package>> orderBy = null, int? page = null, int? pageSize = null, Func<IQueryable<Package>, IIncludableQueryable<Package, object>> includeProperties = null, Expression<Func<Package, Package>>? select = null)
        {
            IQueryable<Package> query = dbSet;

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
                int totalPages = (int)Math.Ceiling((double)count / pageSize.Value);

                // Check if the requested page is valid
                if (page.Value > totalPages)
                {
                    // Return null to indicate no more data
                    return (null, count);
                }

                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            List<Package> data = await query.ToListAsync();
            return (data, count);
        }

    }
}
