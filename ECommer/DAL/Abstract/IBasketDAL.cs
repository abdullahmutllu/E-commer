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
    public interface IBasketDAL : IGenericRepository<Basket>
    {
        Task<bool> BasketAddOrUpdate(Basket basket);
        Task<int> CountByUser(object userId);
        Task<IEnumerable<BasketDTO>> GetBasketDto(object userId);
    }
}
