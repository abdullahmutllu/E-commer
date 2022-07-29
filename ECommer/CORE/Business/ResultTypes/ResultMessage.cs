using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Business.ResultTypes
{
    public class ResultMessage
    {
        public readonly object message;
        public readonly ResultType resultType;

        public ResultMessage(object message, ResultType resultType = ResultType.Success)
        {
            this.message = message;
            this.resultType = resultType;
        }
    }
    public class ResultMessage<T> : ResultMessage
    {
        public readonly T data;

        public ResultMessage(T data, object message, ResultType resultType = ResultType.Success) : base(message, resultType)
        {
            this.data = data;
        }
    }
}
