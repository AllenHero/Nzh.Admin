using Nzh.Admin.Model.Base;
using Nzh.Admin.Model.ExtAttributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Admin.Model
{
    /// <summary>
    ///  Demo
    /// </summary>
    [Table("Demo")]
    public class Demo 
    {
        /// <summary>
        ///Id 
        /// </summary>
        [Key(true)]
        public long Id { get; set; }

        /// <summary>
        /// Name 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Sex
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// Age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }
    }
}
