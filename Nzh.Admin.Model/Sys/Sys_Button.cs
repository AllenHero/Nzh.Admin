using Nzh.Admin.Model.BaseModel;
using Nzh.Admin.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Admin.Model.Sys
{
    /// <summary>
    /// 按钮权限
    /// </summary>
    public class Sys_Button : BaseEntity
    {
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string  ButtonName { get; set; }

        /// <summary>
        /// 按钮样式
        /// </summary>
        public string ButtonIcon { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Status? ButtonStatus { get; set; }
    }
}
