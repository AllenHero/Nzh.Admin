using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Nzh.Admin.IService.Sys;
using Nzh.Admin.Model.Base;
using Nzh.Admin.Model.Enum;
using Nzh.Admin.Model.Sys;
using Nzh.Admin.Web.Controllers;
using Nzh.Admin.Web.Models;

namespace Nzh.Master.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUserService _userService;

        private readonly ILogService _logService;

        public LoginController(ILogger<HomeController> logger, IUserService userService, ILogService logService)
        {
            _logger = logger;
            _userService = userService;
            _logService = logService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult LoginIn(string UserName,string Password)
        {
            //var result = new OperationResult<bool>();
            dynamic result = null;
            //var UserName = request.Value<string>("UserName");
            //string Password = request.Value<string>("Password");
            //var UserName = "";
            //string Password = "";
            if (string.IsNullOrEmpty(UserName))
            {
                result.msg = "用户名不能为空。";
                result.code = -1;
                result.data = false;
                return Json(result);
            }
            if (string.IsNullOrEmpty(Password))
            {
                result.msg = "密码不能为空。";
                result.code = -1;
                result.data = false;
                return Json(result);
            }
            result = _userService.CheckLogin(UserName, Password);

            if (result.code == 1)
            {
                //_userService.UpDateUser(result.data);
            }
            string IpAddress = "";//TODO
            Sys_Log log = new Sys_Log
            {
                LogStatus = result.code == 1 ? Status.Enable : Status.Enable,
                LogType = LogType.LoginSucess,
                Remark = result.msg,
                IpAddress = IpAddress,
                CreateUserId = result.data.Id
            };
            _logService.WriteLog(log);
            return Json(result);
        }
    }
}
