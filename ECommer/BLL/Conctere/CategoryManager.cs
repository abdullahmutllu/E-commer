using BLL.Abstarct;
using CORE.Business;
using CORE.Business.ResultTypes;
using DAL.Abstract;
using ENTİTY.Concrete.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Conctere
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDAL categoryDAL;

        public CategoryManager(ICategoryDAL categoryDAL)
        {
            this.categoryDAL = categoryDAL;
        }
        public ResultMessage<bool> Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        public ResultMessage<bool> Delete(object id)
        {
            throw new NotImplementedException();
        }

        public ResultMessage<Category> Get(object id)
        {
            throw new NotImplementedException();
        }

        public ResultMessage<IEnumerable<Category>> GetList()
        {
            try
            {
                var result = categoryDAL.GetList().Result;
                if (result != null && result.Count() > 0)
                {
                    return new ResultMessage<IEnumerable<Category>>(result, "Success", ResultType.Success);
                }
                return new ResultMessage<IEnumerable<Category>>(null, "Kategory Bulunamadı", ResultType.Notfound);
            }
            catch (Exception ex)
            {
                return new ResultMessage<IEnumerable<Category>>(null, ex.ToInnest().Message, ResultType.Error);
            }
        }

        public ResultMessage<IEnumerable<Category>> GetList(Expression<Func<Category, bool>> func = null, params string[] inculde)
        {
            throw new NotImplementedException();
        }

        public ResultMessage<IEnumerable<Category>> GetList(int skip, int take, Expression<Func<Category, bool>> func = null, params string[] inculde)
        {
            throw new NotImplementedException();
        }

        public ResultMessage Insert(Category entity)
        {
            throw new NotImplementedException();
        }

        public ResultMessage<Category> Update(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
