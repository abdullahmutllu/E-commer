using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTİTY.Concrete.POCO
{
   public class AppUser : IdentityUser<int>
    {
        public virtual ICollection<Basket> Basket { get; set; }
    }
}
