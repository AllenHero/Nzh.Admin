using Nzh.Admin.IService.Base;
using Nzh.Admin.Model.Sys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Admin.IService.Sys
{
    public interface IUserService : IBaseService
    {
        dynamic CheckLogin(string UserName, string Password);

        dynamic GetUserByUserName(string UserName);

        dynamic UpDateUser(Sys_User user);
    }
}
