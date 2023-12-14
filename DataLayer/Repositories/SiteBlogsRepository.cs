using DataLayer.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DbContext;
using DataLayer.Shared;

namespace DataLayer.Repositories
{
    public class SiteBlogsRepository
    {
        protected readonly MainDbContext context;
        private readonly DbSet<Blog> dbSet;

        public SiteBlogsRepository(IEfContextFactory efContextFactory)
        {
            this.context = efContextFactory.Create();
            this.dbSet = context.Set<Blog>();
        }

        public async Task<(IList<Blog> EntityData, int Count)> ListWithPaging(Expression<Func<Blog, bool>> filter = null, Func<IQueryable<Blog>, IOrderedQueryable<Blog>> orderBy = null, int? page = null, int? pageSize = null, Func<IQueryable<Blog>, IIncludableQueryable<Blog, object>> includeProperties = null, Expression<Func<Blog, Blog>>? select = null)
        {
            IQueryable<Blog> query = dbSet;

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

            List<Blog> data = await query.ToListAsync();
            return (data, count);
        }
    }
}
