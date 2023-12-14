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
    public class ReviewsRepository
    {
        protected readonly MainDbContext context;
        private readonly DbSet<Review> dbSet;

        public ReviewsRepository(IEfContextFactory efContextFactory)
        {
            this.context = efContextFactory.Create();
            this.dbSet = context.Set<Review>();
        }

        public virtual async Task Add(Review entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entry = dbSet.Add(entity);

            _ = await context.SaveChangesAsync();
        }


        public async Task<(IList<Review> EntityData, int Count)> ListWithPaging(Expression<Func<Review, bool>> filter = null, Func<IQueryable<Review>, IOrderedQueryable<Review>> orderBy = null, int? page = null, int? pageSize = null, Func<IQueryable<Review>, IIncludableQueryable<Review, object>> includeProperties = null, Expression<Func<Review, Review>>? select = null)
        {
            IQueryable<Review> query = dbSet;

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

            List<Review> data = await query.ToListAsync();
            return (data, count);
        }
    }
}
