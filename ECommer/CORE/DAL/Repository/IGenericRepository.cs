using CORE.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DAL.Repository
{
    public interface IGenericRepository<T>
       where T : class, IBaseEntity, new()
    {
        Task<T> Insert(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Delete(object id);
        Task<T> Update(T entity);
        Task<T> Get(object id);
        Task<IEnumerable<T>> GetList();
        Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> func = null, params string[] inculde);
        Task<IEnumerable<T>> GetList(int skip, int take, Expression<Func<T, bool>> func = null, params string[] inculde);
    }
}
