using Nzh.Admin.IRepository.Sys;
using Nzh.Admin.IService.Sys;
using Nzh.Admin.Model.Base;
using Nzh.Admin.Model.Sys;
using Nzh.Admin.Service.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Nzh.Admin.Service.Sys
{
    public class LogService : BaseService, ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public OperationResult<bool> WriteLog(Sys_Log log)
        {
            using (IDbTransaction tran = _logRepository.BeginTransaction())//开始事务
            {
                var result = new OperationResult<bool>();
                try
                {
                    byte[] buffer = Guid.NewGuid().ToByteArray();
                    Sys_Log logModel = new Sys_Log();
                    logModel.Id = BitConverter.ToInt64(buffer, 0);
                    logModel.CreateUserId = log.CreateUserId;
                    logModel.IpAddress = log.IpAddress;
                    logModel.CreateTime = DateTime.Now;
                    logModel.LogStatus = log.LogStatus;
                    logModel.LogType = log.LogType;
                    logModel.Remark = log.Remark;
                    result.data = _logRepository.Insert(logModel);
                    _logRepository.CommitTransaction(tran);//提交事务
                    return result;
                }
                catch (Exception ex)
                {
                    _logRepository.RollbackTransaction(tran);//回滚事务
                    throw ex;
                }
            }
        }
    }
}
