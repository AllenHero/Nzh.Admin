using Nzh.Admin.Model.BaseModel;
using Nzh.Admin.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Admin.Model.Sys
{
    /// <summary>
    /// 岗位
    /// </summary>
    public class Sys_Position : BaseEntity
    {
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// 上级岗位Id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Status? PositionStatus { get; set; }
    }
}
