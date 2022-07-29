using CORE.Business.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstarct
{
    public interface IGenericService<T>
    {
        ResultMessage Insert(T entity);
        ResultMessage<bool> Delete(T entity);
        ResultMessage<bool> Delete(object id);
        ResultMessage<T> Update(T entity);
        ResultMessage<T> Get(object id);
        ResultMessage<IEnumerable<T>> GetList();
        ResultMessage<IEnumerable<T>> GetList(Expression<Func<T, bool>> func = null, params string[] inculde);
        ResultMessage<IEnumerable<T>> GetList(int skip, int take, Expression<Func<T, bool>> func = null, params string[] inculde);

    }
}
