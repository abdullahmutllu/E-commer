using DAL.Abstract;
using DAL.Concrete.EntityFramework.Database;
using ENTİTY.Concrete.POCO;
using ENTİTY.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete.EntityFramework
{
    public class EfBasketRepo : EfRepository<Basket, MyProjectDbContext>, IBasketDAL
    {
        private readonly MyProjectDbContext db;

        public EfBasketRepo(MyProjectDbContext db) : base(db)
        {
            this.db = db;
        }

        public async Task<bool> BasketAddOrUpdate(Basket basket)
        {
            bool ok = false;
            var strategy = db.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var basketDb = db.Basket.FirstOrDefault(x => x.Active == true && x.Deleted == false
                        && x.AppUserId == basket.AppUserId && x.ProductId == basket.ProductId);
                        if (basketDb == null)
                        {
                            //Add
                            db.Basket.Add(basket);
                        }
                        else
                        {
                            basketDb.Count += basketDb.Count;
                            db.Basket.Update(basketDb);
                            //Or
                        }
                        var result = db.SaveChanges();
                        if (result == 0)
                            transaction.Rollback();
                        else
                        {
                            ok = true;
                            transaction.Commit();
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            });

            return await Task.FromResult(ok);
        }

        public async Task<int> CountByUser(object userId)
        {
            var count = db.Basket.Where(x => x.AppUserId == (int)userId).Count();
            return await Task.FromResult(count);
        }

        public async Task<IEnumerable<BasketDTO>> GetBasketDto(object userId)
        {

            var result = from basket in db.Basket
                         join product in db.Product on basket.ProductId equals product.Id
                         where product.Active == true
                         && product.Deleted == false && product.Stok > 0
                         && basket.Active == true && basket.Deleted == false
                         && basket.AppUserId == Convert.ToInt32(userId)
                         select new BasketDTO
                         {
                             Count = basket.Count,
                             Price = product.Price,
                             ProductId = product.Id,
                             Stok = product.Stok,
                             ProductName = product.Name,
                             ImageUrl = db.ProductImage.FirstOrDefault(x => x.ProductId == product.Id).ImageUrl
                         };
            return await Task.FromResult(result);
        }
    }
}
