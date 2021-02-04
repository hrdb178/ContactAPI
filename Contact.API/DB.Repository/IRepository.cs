using Contact.API.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DB.Repository
{
    public interface IRepository<T> where T : BaseClassFields
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(long id);
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<List<T>> FindResult(Expression<Func<T, bool>> predicate);
        Task<List<T>> FindResult(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<List<T>> FindResult(params Expression<Func<T, object>>[] includeProperties);

        Task<T> Result(Expression<Func<T, bool>> predicate);
        Task<T> Result(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
    }
}
