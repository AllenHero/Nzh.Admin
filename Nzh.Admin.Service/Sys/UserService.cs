using Nzh.Admin.Common.Helper;
using Nzh.Admin.IRepository.Sys;
using Nzh.Admin.IService.Sys;
using Nzh.Admin.Model.Base;
using Nzh.Admin.Model.Enum;
using Nzh.Admin.Model.Sys;
using Nzh.Admin.Service.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Nzh.Admin.Service.Sys
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public dynamic CheckLogin(string UserName,string Password) 
        {
            var result = new OperationResult<Sys_User>();
            Sys_User user = GetUserByUserName(UserName);
            if (user!=null)
            {
                if (user.UserStatus == Status.Enable)
                {
                    if (user.Password == EncryptionHelper.DesEncrypt(Password))
                    {
                        user.LoginCount++;
                        if (user.FirstVisit==null)
                        {
                            user.FirstVisit = DateTime.Now;
                        }
                        user.LastVisit = DateTime.Now;
                        result.code = 1;
                        result.msg = "登录成功。";
                        result.data = user;
                    }
                    else
                    {
                        result.code = -1;
                        result.msg = "密码不正确，请重新输入。";
                        result.data = null;
                    }
                }
                else
                {
                    result.code = -1;
                    result.msg = "账号被禁用，请联系管理员。";
                    result.data = null;
                }
            }
            else
            {
                result.code = -1;
                result.msg = "账号不存在，请重新输入。";
                result.data = null;
            }
            return result;
        }


        public dynamic GetUserByUserName(string UserName)
        {
            string sql = @"SELECT * from  Sys_User where UserName='{0}'";
            sql = string.Format(sql, UserName);
            Sys_User userModel = _userRepository.Get(sql);
            return userModel;
        }

        public dynamic UpDateUser(Sys_User user) 
        {
            using (IDbTransaction tran = _userRepository.BeginTransaction())//开始事务
            {
                var result = new OperationResult<bool>();
                Sys_User useModel = new Sys_User();
                try
                {
                    if (user != null)
                    {
                        useModel.Id = user.Id;
                        useModel.LoginCount = user.LoginCount;
                        useModel.FirstVisit = user.FirstVisit;
                        useModel.LastVisit = user.LastVisit;
                        result.data = _userRepository.Update(useModel);
                        _userRepository.CommitTransaction(tran);//提交事务
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    _userRepository.RollbackTransaction(tran);//回滚事务
                    throw ex;
                }
            }
        }
    }
}
