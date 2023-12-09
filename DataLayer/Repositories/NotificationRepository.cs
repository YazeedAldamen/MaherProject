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
    public class NotificationRepository
    {

        protected readonly MainDbContext context;
        private readonly DbSet<Notification> dbSet;

        public NotificationRepository(IEfContextFactory efContextFactory)
        {
            this.context = efContextFactory.Create();
            this.dbSet = context.Set<Notification>();
        }

        public async Task<(IList<Notification> EntityData, int Count)> ListWithPaging(Expression<Func<Notification, bool>> filter = null, Func<IQueryable<Notification>, IOrderedQueryable<Notification>> orderBy = null, int? page = null, int? pageSize = null, Func<IQueryable<Notification>, IIncludableQueryable<Notification, object>> includeProperties = null, Expression<Func<Notification, Notification>>? select = null)
        {
            IQueryable<Notification> query = dbSet;

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

            List<Notification> data = await query.ToListAsync();
            return (data, count);
        }

    }
}
