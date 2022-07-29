using CORE.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTİTY.Concrete.POCO
{
    public class ProductImage : BaseEntity
    {
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }

        //Nav Prop.
        public virtual Product Product { get; set; }
    }
}
