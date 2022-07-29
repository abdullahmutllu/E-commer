using DAL.Concrete.EntityFramework.Mapping;
using ENTİTY.Concrete.POCO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete.EntityFramework.Database
{//MyProjectDbContext
    public class MyProjectDbContext : IdentityDbContext<AppUser, AppRole, int>
    {


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MyProjectDb;Integrated Security=true");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductCategory>().HasKey(x => new { x.ProductId, x.CategoryId });

            builder.Entity<Product>().Property(x => x.Name)
                .IsRequired()
                .HasColumnName("name");
            builder.Entity<Product>().HasIndex(x => x.Name).IsUnique();
            builder.Entity<AppRole>().HasData(

                new AppRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new AppRole { Id = 2, Name = "User", NormalizedName = "USER" }
                );

            builder.ApplyConfiguration<Category>(new CategoryMap());
            base.OnModelCreating(builder);
        }


        public DbSet<Product> Product { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Basket> Basket { get; set; }
    }
}
