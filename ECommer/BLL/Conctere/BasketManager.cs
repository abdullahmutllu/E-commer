using BLL.Abstarct;
using CORE.Business;
using CORE.Business.ResultTypes;
using DAL.Abstract;
using ENTİTY.Concrete.POCO;
using ENTİTY.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Conctere
{
    public class BasketManager : IBasketService
    {
        private readonly IBasketDAL basketDAL;

        public BasketManager(IBasketDAL basketDAL)
        {
            this.basketDAL = basketDAL;
        }
        public ResultMessage<bool> BasketAddOrUpdate(Basket basket)
        {
            try
            {
                var result = basketDAL.BasketAddOrUpdate(basket).Result;
                if (result)
                {
                    return new ResultMessage<bool>(true, "Success");
                }
                return new ResultMessage<bool>(false, "Warning", ResultType.Warning);
            }
            catch (Exception ex)
            {
                return new ResultMessage<bool>(false, ex.ToInnest().Message, ResultType.Error);
            }
        }

        public ResultMessage<bool> Delete(Basket entity)
        {
            throw new NotImplementedException();
        }

        public ResultMessage<bool> Delete(object id)
        {
            throw new NotImplementedException();
        }

        public ResultMessage<Basket> Get(object id)
        {
            throw new NotImplementedException();
        }

        public ResultMessage<IEnumerable<BasketDTO>> GetBasketDto(object userId)
        {
            try
            {
                if (userId == null)
                {
                    return new ResultMessage<IEnumerable<BasketDTO>>(null, "NotValidaiton", ResultType.NotValidaiton);
                }
                var result = basketDAL.GetBasketDto(userId).Result;
                if (result.Count() > 0)
                {
                    return new ResultMessage<IEnumerable<BasketDTO>>(result, "Success");
                }
                return new ResultMessage<IEnumerable<BasketDTO>>(null, "Sepet Boş Ürün Ekle", ResultType.Notfound);
            }
            catch (Exception ex)
            {
                return new ResultMessage<IEnumerable<BasketDTO>>(null, ex.ToInnest().Message, ResultType.Error);
            }
        }

        public ResultMessage<IEnumerable<Basket>> GetList()
        {
            throw new NotImplementedException();
        }

        public ResultMessage<IEnumerable<Basket>> GetList(Expression<Func<Basket, bool>> func = null, params string[] inculde)
        {
            try
            {
                var result = basketDAL.GetList(func, inculde).Result;
                return new ResultMessage<IEnumerable<Basket>>(result, "Success");
            }
            catch (Exception ex)
            {
                return new ResultMessage<IEnumerable<Basket>>(null, ex.ToInnest().Message, ResultType.Error);
            }
        }

        public ResultMessage<IEnumerable<Basket>> GetList(int skip, int take, Expression<Func<Basket, bool>> func = null, params string[] inculde)
        {
            throw new NotImplementedException();
        }

        public ResultMessage Insert(Basket entity)
        {
            throw new NotImplementedException();
        }

        public ResultMessage<Basket> Update(Basket entity)
        {
            throw new NotImplementedException();
        }
    }
}
