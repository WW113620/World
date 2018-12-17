using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceWorld.Model
{
    public class CallBackResult
    {
    }
    public class ResultModel
    {
        public int errorCode { get; set; }
        public string errorMes { get; set; }

    }

    public class ResultData<T> : ResultModel
    {
        public T data { get; set; }
    }

}
