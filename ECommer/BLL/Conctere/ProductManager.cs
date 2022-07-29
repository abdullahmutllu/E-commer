using BLL.Abstarct;
using BLL.Validation;
using Core.Business;
using CORE.Business;
using CORE.Business.ResultTypes;
using CORE.Logger;
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
    public class ProductManager : IProductService
    {
        private readonly IProductDAL productDAL;

        public ProductManager(IProductDAL productDAL)
        {
            this.productDAL = productDAL;
        }
        public ResultMessage<bool> Delete(Product entity)
        {
            try
            {
                if (productDAL.Delete(entity).Result)
                {
                    return new ResultMessage<bool>(true, "Succuess", ResultType.Success);
                }
                return new ResultMessage<bool>(false, "Warning", ResultType.Warning);
            }
            catch (Exception ex)
            {
                return new ResultMessage<bool>(false, ex.ToInnest().Message, ResultType.Error);
            }
        }

        public ResultMessage<bool> Delete(object id)
        {
            try
            {
                if (productDAL.Delete(id).Result)
                {
                    return new ResultMessage<bool>(true, "Succuess", ResultType.Success);
                }
                return new ResultMessage<bool>(false, "Warning", ResultType.Warning);
            }
            catch (Exception ex)
            {
                return new ResultMessage<bool>(false, ex.ToInnest().Message, ResultType.Error);
            }
        }

        public ResultMessage<Product> Get(object id)
        {
            throw new NotImplementedException();
        }

        public ResultMessage<IEnumerable<Product>> GetList()
        {
            throw new NotImplementedException();
        }

        public ResultMessage<IEnumerable<Product>> GetList(Expression<Func<Product, bool>> func = null, params string[] inculde)
        {
            throw new NotImplementedException();
        }

        public ResultMessage<IEnumerable<Product>> GetList(int skip, int take, Expression<Func<Product, bool>> func = null, params string[] inculde)
        {
            throw new NotImplementedException();
        }

        public ResultMessage<IEnumerable<ProductDto>> GetProductListByCategoryId(int categoryId)
        {
            try
            {
                var result = productDAL.GetProductListByCategoryId(categoryId);
                if (result.Count() > 0)
                {
                    return new ResultMessage<IEnumerable<ProductDto>>(result, "Ok", ResultType.Success);
                }
                return new ResultMessage<IEnumerable<ProductDto>>(null, "YOĞ", ResultType.Notfound);
            }
            catch (Exception ex)
            {
                return new ResultMessage<IEnumerable<ProductDto>>(null, ex.ToInnest().Message, ResultType.Error);
            }
        }

        public ResultMessage Insert(Product entity)
        {
            try
            {
                var product = productDAL.Update(entity).Result;
                if (product != null)
                    return new ResultMessage<Product>(product, ResponseMessage.Add, ResultType.Success);
                return new ResultMessage<Product>(null, "Warning", ResultType.Warning);
            }
            catch (Exception ex)
            {
                return new ResultMessage<Product>(null, ex.ToInnest().Message, ResultType.Error);
            }
        }
        public ResultMessage<bool> ProductCreate(ProductCreateDTO model)
        {
            try
            {
               
                ProductDtoValidation validations = new ProductDtoValidation(productDAL);
                var val = validations.Validate(model);
                if (!val.IsValid)
                {
                    Nlog.LogErrorFile($"{DateTime.Now.ToShortDateString()} Hata", @"C:\Users\Vektörel\Desktop\ErrorLog\hata.txt");
                    return new ResultMessage<bool>(false, val.Errors, ResultType.NotValidaiton);
                }
                if (productDAL.ProductCreate(model).Result)
                {
                    return new ResultMessage<bool>(true, "Succuess", ResultType.Success);
                }
                return new ResultMessage<bool>(false, "Warning", ResultType.Warning);
            }
            catch (Exception ex)
            {
                return new ResultMessage<bool>(false, ex.ToInnest().Message, ResultType.Error);
            }
        }

        public ResultMessage<Product> Update(Product entity)
        {
            try
            {
                var product = productDAL.Update(entity).Result;
                if (product != null)
                {
                    return new ResultMessage<Product>(product, "Succuess", ResultType.Success);
                }
                return new ResultMessage<Product>(null, "Warning", ResultType.Warning);
            }
            catch (Exception ex)
            {
                return new ResultMessage<Product>(null, ex.ToInnest().Message, ResultType.Error);
            }
        }
    }
}
