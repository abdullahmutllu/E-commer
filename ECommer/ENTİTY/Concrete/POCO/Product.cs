using CORE.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTİTY.Concrete.POCO
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stok { get; set; }
        public string Description { get; set; }
        //Nav Prop
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductCategory> ProductCategory { get; set; }
        public virtual ICollection<Basket> Basket { get; set; }
    }
}
