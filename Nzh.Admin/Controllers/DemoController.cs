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
    [Route("api/[controller]/[action]")]
    //[ApiController]
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
        /// 获取所有Demo
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDemoPageList")]
        public async Task<JsonResult> GetDemoPageList(int PageIndex, int PageSize)
        {
            var result = new OperationResult<List<Demo>>();
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
        /// 获取单个Demo
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet("GetDemoById")]
        public async Task<JsonResult> GetDemoById(Guid ID)
        {
            var result = new OperationResult<Demo>();
            try
            {
                result.data = await _demoService.GetDemoById(ID);
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
        /// 新增Demo
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("AddDemo")]
        public async Task<JsonResult> AddDemo(Demo entity)
        {
            var result = new OperationResult<bool>();
            try
            {
                result = await _demoService.AddDemo(entity);
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
        /// 修改Demo
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("UpdateDemo")]
        public async Task<JsonResult> UpdateDemo(Demo entity)
        {
            var result = new OperationResult<bool>();
            try
            {
                result = await _demoService.UpdateDemo(entity);
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
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete("DeleteDemo")]
        public async Task<JsonResult> DeleteDemo(Guid ID)
        {
            var result = new OperationResult<bool>();
            try
            {
                result = await _demoService.DeleteDemo(ID);
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