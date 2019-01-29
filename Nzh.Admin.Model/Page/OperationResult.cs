using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Admin.Model.Page
{
    public class OperationResult
    {
        public int code { get; set; }

        public string msg { get; set; }
    }

    public class OperationResult<T> : OperationResult
    {
        public OperationResult()
        {
            code = 0;
            msg = "成功";
        }

        public T data { get; set; }
    }

}
