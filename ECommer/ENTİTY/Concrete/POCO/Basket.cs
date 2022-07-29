using CORE.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTİTY.Concrete.POCO
{
    public class Basket : BaseEntity
    {
        public int ProductId { get; set; }
        public int AppUserId { get; set; }
        public int Count { get; set; }
        public virtual Product Product { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
