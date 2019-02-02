using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Admin.Model.Base
{
    /// <summary>
    /// 基础实体类
    /// </summary>
    public class BaseModel : IEntity<Guid>
    {
        public Guid ID { get; set; }
    }
}
