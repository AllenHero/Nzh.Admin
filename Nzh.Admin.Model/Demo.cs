using Nzh.Admin.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Admin.Model
{
    public class Demo : BaseModel
    {
        public string Name { get; set; }

        public string Sex { get; set; }

        public int Age { get; set; }

        public string Remark { get; set; }
    }
}
