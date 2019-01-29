using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Admin.Model.Base
{
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey ID { get; set; }
    }
}
