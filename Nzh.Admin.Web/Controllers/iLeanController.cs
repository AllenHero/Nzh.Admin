using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nzh.Admin.Web.Controllers
{
    public class iLeanController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            //var loginer = FormsAuth.GetUserData<LoginerBase>();
            //string SysName = new Areas.Sys.Controllers.ParameterApiController().GetValueByCode(loginer.TenantId, "SysName");
            //SysDataCommand.IsAuthorChange();

            //if (!string.IsNullOrEmpty(SysName))
            //{
            //    ViewBag.Title = SysName;
            //}
            //else
            //{
            //    ViewBag.Title = "ILean2.0";
            //}
            //ViewBag.UserName = loginer.UserName;
            //ViewBag.UserCode = loginer.UserCode;
            //ViewBag.TenantId = loginer.TenantId;
            //if (string.IsNullOrEmpty(loginer.TenantId))
            //{
            //    Response.Redirect("/");
            //}

            var result = new Dictionary<string, object>();
            result.Add("theme", "gray");
            result.Add("navigation", "accordion");
            result.Add("gridrows", "20");

            ViewBag.Settings = result;
            return View();
        }
    }
}
