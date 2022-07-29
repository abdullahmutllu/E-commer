using CORE.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTİTY.Concrete.POCO
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        //Navigation Property
        public virtual ICollection<ProductCategory> ProductCategory { get; set; }

    }
}
