using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Business
{
    public static class InnerEx
    {
        public static Exception ToInnest(this Exception exception)
        {
            if (exception.InnerException != null)
            {
                return exception.InnerException.ToInnest();
            }
            return exception;
        }
    }
}
