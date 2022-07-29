using CORE.DAL.Repository;
using ENTİTY.Concrete.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface ICategoryDAL : IGenericRepository<Category>
    {
    }
}
