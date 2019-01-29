using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Admin.Model.Base
{
    public class BaseModel : IEntity<Guid>
    {
        public Guid ID { get; set; }
    }
}
