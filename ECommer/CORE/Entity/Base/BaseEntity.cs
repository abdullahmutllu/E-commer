using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Entity.Base
{
    public interface IBaseEntity { }
    public class BaseEntity : IBaseEntity
    {
        public BaseEntity()
        {
            Active = true;
            Deleted = false;
            Created = DateTime.Now;
            Update = DateTime.Now;
        }
        public int Id { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime Update { get; set; }
    }
}
