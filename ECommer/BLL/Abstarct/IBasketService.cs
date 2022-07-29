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
    public interface IBasketService : IGenericService<Basket>
    {
        ResultMessage<bool> BasketAddOrUpdate(Basket basket);
        ResultMessage<IEnumerable<BasketDTO>> GetBasketDto(object userId);
    }
}
