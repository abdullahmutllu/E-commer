using CORE.DAL.Repository;
using CORE.Entity.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete.EntityFramework
{
    public class EfRepository<TEntity, TDbContext> : IGenericRepository<TEntity>
         where TEntity : class, IBaseEntity, new()
         where TDbContext : DbContext

    {
        private readonly DbContext db;

        public EfRepository(DbContext db)
        {
            this.db = db;
        }

        public async Task<bool> Delete(TEntity entity)
        {
            db.Remove(entity);
            var result = await db.SaveChangesAsync();
            return result > 0 ? true : false;
        }

        public async Task<bool> Delete(object id)
        {
            var entity = db.Set<TEntity>().FindAsync(id);

            entity.GetType().GetProperty("Active").SetValue(entity, false);
            entity.GetType().GetProperty("Deleted").SetValue(entity, true);
            db.Update(entity);
            var result = await db.SaveChangesAsync();
            return result > 0 ? true : false;
            //foreach (var item in entity.GetType().GetProperties())
            //{
            //    if (item.Name=="Active")
            //    {
            //        item.SetValue(entity, false);
            //    }
            //    else if (item.Name == "Deleted")
            //    {
            //        item.SetValue(entity, true);
            //    }
            //}
        }

        public async Task<TEntity> Get(object id)
        {
            return await db.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetList()
        {
            var result = db.Set<TEntity>().AsNoTracking();

            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<TEntity>> GetList(Expression<Func<TEntity, bool>> func = null,
                                                  params string[] inculde)
        {

            IQueryable<TEntity> result =
                func == null ? db.Set<TEntity>() : db.Set<TEntity>().Where(func);//Sorgu Hazırlanıyor
            if (inculde.Length == 0)
            {
                //var aa = result.ToQueryString();
                return await Task.FromResult(result.AsNoTracking());
            }
            else
            {
                foreach (var item in inculde)
                    result = result.Include(item);
                var aa = result.ToQueryString();
                return await Task.FromResult(result.AsNoTracking());
            }
        }
        /// <summary>
        /// Bu Method da Sayfalama İşlemleri yapılcaktır
        /// </summary>
        /// <param name="skip">Zorunlu Parametre</param>
        /// <param name="take">Zorunlu Parametre</param>
        /// <param name="func">Optional</param>
        /// <param name="inculde"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetList(int skip, int take, Expression<Func<TEntity, bool>> func = null, params string[] inculde)
        {
            IQueryable<TEntity> result =
                func == null ? db.Set<TEntity>() : db.Set<TEntity>().Where(func);//Sorgu Hazırlanıyor
            if (inculde.Length == 0)
            {
                return await Task.FromResult(result.Skip(skip).Take(take).AsNoTracking());
            }
            else
            {
                foreach (var item in inculde)
                    result = result.Include(item);
                return await Task.FromResult(result.Skip(skip).Take(take).AsNoTracking());
            }
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            db.Entry(entity).State = EntityState.Added;
            //db.Add(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            //db.Update(entity);
            await db.SaveChangesAsync();
            return entity;
        }
    }
}
