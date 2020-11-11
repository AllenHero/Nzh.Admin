using Nzh.Admin.Model.BaseModel;
using Nzh.Admin.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Admin.Model.Sys
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Sys_Role : BaseEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Status? RoleStatus { get; set; }
    }
}
