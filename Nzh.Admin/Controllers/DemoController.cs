using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Nzh.Admin.IService;
using Nzh.Admin.Model;
using Nzh.Admin.Model.Base;
using STD.NetCore.Common;

namespace Nzh.Admin.Controllers
{
    /// <summary>
    /// Demo
    /// </summary>
    [Produces("application/json")]
    [Route("api/Demo")]
    public class DemoController : Controller
    {
        private readonly IDemoService _demoService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="demoService"></param>
        public DemoController(IDemoService demoService)
        {
            _demoService = demoService;
        }

        /// <summary>
        /// 获取Demo分页
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [HttpGet("GetDemoPageList")]
        public async Task<JsonResult> GetDemoPageList(int PageIndex, int PageSize)
        {
            var result = new OperationResult<PageResult<Demo>>();
            try
            {
                result.data = await _demoService.GetDemoPageList(PageIndex, PageSize);
            }
            catch (Exception ex)
            {
                result.code = -1;
                result.msg = ex.Message;
            }
            Logger.Info(JsonConvert.SerializeObject(result)); //此处调用日志记录函数记录日志
            return Json(result);
        }

        /// <summary>
        /// 获取Demo
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetDemoById")]
        public async Task<JsonResult> GetDemoById(long Id)
        {
            var result = new OperationResult<Demo>();
            try
            {
                result.data = await _demoService.GetDemoById(Id);
            }
            catch (Exception ex)
            {
                result.code = -1;
                result.msg = ex.Message;
            }
            Logger.Info(JsonConvert.SerializeObject(result)); //此处调用日志记录函数记录日志
            return Json(result);
        }

        /// <summary>
        /// 添加Demo
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Sex"></param>
        /// <param name="Age"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        [HttpPost("AddDemo")]
        public async Task<JsonResult> AddDemo(string Name, string Sex, int Age, string Remark)
        {
            var result = new OperationResult<bool>();
            try
            {
                result = await _demoService.AddDemo(Name, Sex, Age, Remark);
            }
            catch (Exception ex)
            {
                result.code = -1;
                result.msg = ex.Message;
            }
            Logger.Info(JsonConvert.SerializeObject(result)); //此处调用日志记录函数记录日志
            return Json(result);
        }

        /// <summary>
        ///  修改Demo
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Sex"></param>
        /// <param name="Age"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        [HttpPut("UpdateDemo")]
        public async Task<JsonResult> UpdateDemo(long Id, string Name, string Sex, int Age, string Remark)
        {
            var result = new OperationResult<bool>();
            try
            {
                result = await _demoService.UpdateDemo(Id, Name, Sex, Age, Remark);
            }
            catch (Exception ex)
            {
                result.code = -1;
                result.msg = ex.Message;
            }
            Logger.Info(JsonConvert.SerializeObject(result)); //此处调用日志记录函数记录日志
            return Json(result);
        }

        /// <summary>
        /// 删除Demo
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteDemo")]
        public async Task<JsonResult> DeleteDemo(long Id)
        {
            var result = new OperationResult<bool>();
            try
            {
                result = await _demoService.DeleteDemo(Id);
            }
            catch (Exception ex)
            {
                result.code = -1;
                result.msg = ex.Message;
            }
            Logger.Info(JsonConvert.SerializeObject(result)); //此处调用日志记录函数记录日志
            return Json(result);
        }
    }
}