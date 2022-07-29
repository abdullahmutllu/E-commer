using CORE.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTİTY.Concrete.POCO
{
    public class ProductCategory : BaseEntity
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }


        //Nav Porp.

        public virtual Product Product { get; set; }
        public virtual Category Category { get; set; }
    }
}
