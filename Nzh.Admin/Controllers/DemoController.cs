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
        [HttpGet("GetDemoPageListAsync")]
        public async Task<JsonResult> GetDemoPageListAsync(int PageIndex, int PageSize)
        {
            var result = new OperationResult<PageResult<Demo>>();
            try
            {
                result.data = await _demoService.GetDemoPageListAsync(PageIndex, PageSize);
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
        [HttpGet("GetDemoByIdAsync")]
        public async Task<JsonResult> GetDemoByIdAsync(long Id)
        {
            var result = new OperationResult<Demo>();
            try
            {
                result.data = await _demoService.GetDemoByIdAsync(Id);
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
        [HttpPost("InsertDemoAsync")]
        public async Task<JsonResult> InsertDemoAsync(string Name, string Sex, int Age, string Remark)
        {
            var result = new OperationResult<bool>();
            try
            {
                result = await _demoService.InsertDemoAsync(Name, Sex, Age, Remark);
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
        [HttpPut("UpdateDemoAsync")]
        public async Task<JsonResult> UpdateDemoAsync(long Id, string Name, string Sex, int Age, string Remark)
        {
            var result = new OperationResult<bool>();
            try
            {
                result = await _demoService.UpdateDemoAsync(Id, Name, Sex, Age, Remark);
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
        [HttpDelete("DeleteDemoAsync")]
        public async Task<JsonResult> DeleteDemoAsync(long Id)
        {
            var result = new OperationResult<bool>();
            try
            {
                result = await _demoService.DeleteDemoAsync(Id);
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