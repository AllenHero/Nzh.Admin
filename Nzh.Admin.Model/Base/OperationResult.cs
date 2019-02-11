using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Admin.Model.Base
{
    /// <summary>
    /// 返回通用类
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }
    }

    /// <summary>
    ///  返回通用类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OperationResult<T> : OperationResult
    {
        /// <summary>
        /// 返回通用类
        /// </summary>
        public OperationResult()
        {
            code = 0;
            msg = "成功";
        }

        /// <summary>
        /// 数据
        /// </summary>
        public T data { get; set; }
    }
}
