using DAL.Abstract;
using DAL.Concrete.EntityFramework.Database;
using ENTİTY.Concrete.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete.EntityFramework
{
    public class EfCategoryRepo : EfRepository<Category, MyProjectDbContext>, ICategoryDAL
    {
        private readonly MyProjectDbContext db;

        public EfCategoryRepo(MyProjectDbContext db) : base(db)
        {
            this.db = db;
        }
    }
}
