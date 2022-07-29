using CORE.DAL.Repository;
using ENTİTY.Concrete.POCO;
using ENTİTY.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IProductDAL : IGenericRepository<Product>
    {
        Task<bool> ProductCreate(ProductCreateDTO model);
        IEnumerable<ProductDto> GetProductListByCategoryId(int categoryId);
    }
}
