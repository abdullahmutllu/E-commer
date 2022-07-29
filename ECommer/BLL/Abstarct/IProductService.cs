using CORE.Business.ResultTypes;
using ENTİTY.Concrete.POCO;
using ENTİTY.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstarct
{
    public interface IProductService : IGenericService<Product>
    {
        ResultMessage<bool> ProductCreate(ProductCreateDTO model);
        ResultMessage<IEnumerable<ProductDto>> GetProductListByCategoryId(int categoryId);
    }
}
