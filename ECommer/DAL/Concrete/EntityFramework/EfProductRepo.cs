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
    public class EfProductRepo : EfRepository<Product, MyProjectDbContext>, IProductDAL
    {
        private readonly MyProjectDbContext db;

        public EfProductRepo(MyProjectDbContext db) : base(db)
        {
            this.db = db;
        }

        public IEnumerable<ProductDto> GetProductListByCategoryId(int categoryId)
        {
            var result =
                from product in db.Product
                join proCategory in db.ProductCategory on product.Id equals proCategory.ProductId
                join category in db.Category on proCategory.CategoryId equals category.Id
                where product.Active == true && product.Deleted == false
                && proCategory.CategoryId == categoryId
                select new ProductDto
                {
                    ProductId = product.Id,
                    CatagoryName = category.Name,
                    CategoryId = category.Id,
                    Stok = product.Stok,
                    Price = product.Price,
                    Name = product.Name,
                    Imageurl = db.ProductImage.FirstOrDefault(x => x.ProductId == product.Id).ImageUrl
                };
            return result;
        }

     
        public async Task<bool> ProductCreate(ProductCreateDTO model)
        {
            bool ok = false;
            var strategy = db.Database.CreateExecutionStrategy();

            strategy.Execute(() =>
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Product product = new Product()
                        {
                            Description = model.Description,
                            Name = model.Name,
                            Price = model.Price,
                            Stok = model.Stok,
                        };
                        db.Product.Add(product);
                        var productResult = db.SaveChanges();
                        if (productResult == 0) transaction.Rollback();
                        else
                        {
                            ProductCategory productCategory = new ProductCategory
                            {
                                CategoryId = model.CategoryId,
                                ProductId = product.Id
                            };
                            db.ProductCategory.Add(productCategory);

                            foreach (var item in model.ImageUrl)
                            {
                                ProductImage productImage = new ProductImage
                                {
                                    ImageUrl = item,
                                    ProductId = product.Id
                                };
                                db.ProductImage.Add(productImage);
                            }

                            var result = db.SaveChanges();
                            if (result == 0)
                            {
                                transaction.Rollback();
                            }
                            else
                            {
                                ok = true;
                                transaction.Commit();
                            }
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
    }
}
