using Contact.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DB.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseClassFields
    {
        private readonly ContactDbContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;
        public Repository(ContactDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await entities.ToListAsync().ConfigureAwait(false);
        }
        public async Task<T> Get(long id)
        {
            return await entities.SingleOrDefaultAsync(s => s.UniqueId == id).ConfigureAwait(false);
        }
        public async Task Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Add(entity);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }
        public async Task Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await context.SaveChangesAsync().ConfigureAwait(false);
        }
        public async Task Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<List<T>> FindResult(Expression<Func<T, bool>> predicate)
        {
            return await entities.Where(predicate).ToListAsync().ConfigureAwait(false);
        }

        public async Task<List<T>> FindResult(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = entities.Where(predicate).AsQueryable();

            includeProperties.ToList().ForEach(
                navitable =>
                {
                    if (navitable != null)
                    {
                        query = includeProperties.Aggregate(query,
                                                            (current, expression) => current.Include(navitable));

                    }
                });

            return await query.ToListAsync().ConfigureAwait(false);

        }
        public async Task<List<T>> FindResult(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = entities.AsQueryable();

            includeProperties.ToList().ForEach(
                navitable =>
                {
                    if (navitable != null)
                    {
                        query = includeProperties.Aggregate(query,
                                                            (current, expression) => current.Include(navitable));

                    }
                });

            return await query.ToListAsync().ConfigureAwait(false);
        }
        public async Task<T> Result(Expression<Func<T, bool>> predicate)
        {
            return await entities.FirstOrDefaultAsync(predicate).ConfigureAwait(false);
        }
        public async Task<T> Result(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = entities.AsQueryable();

            includeProperties.ToList().ForEach(
                navitable =>
                {
                    if (navitable != null)
                    {
                        query = includeProperties.Aggregate(query,
                                                            (current, expression) => current.Include(navitable));

                    }
                });

            return await query.FirstOrDefaultAsync(predicate).ConfigureAwait(false);
        }
    }
}
