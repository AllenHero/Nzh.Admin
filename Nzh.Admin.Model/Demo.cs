using Nzh.Admin.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Admin.Model
{
    /// <summary>
    ///  Demo
    /// </summary>
    public class Demo 
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Sex { get; set; }

        public int Age { get; set; }

        public string Remark { get; set; }
    }
}
