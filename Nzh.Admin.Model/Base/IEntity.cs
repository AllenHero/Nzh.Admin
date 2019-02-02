using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Admin.Model.Base
{
    /// <summary>
    ///  接口
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey ID { get; set; }
    }
}
