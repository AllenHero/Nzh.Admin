using Nzh.Admin.IService.Base;
using Nzh.Admin.Model.Base;
using Nzh.Admin.Model.Sys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Admin.IService.Sys
{
    public interface ILogService : IBaseService
    {
        OperationResult<bool> WriteLog(Sys_Log log);
    }
}
