using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Nzh.Admin.IRepository;
using Nzh.Admin.Model;
using STD.NetCore.Common;

namespace Nzh.Admin.Controllers
{
    [Route("api/[controller]/[action]")]
    //[ApiController]
    public class DemoController : Controller
    {
        private readonly IDemoRepository _demoRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="demoRepository"></param>
        public DemoController(IDemoRepository demoRepository)
        {
            _demoRepository = demoRepository;
        }

        /// <summary>
        /// 获取所有Demo
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDemoList")]
        public async Task<JsonResult> GetDemoList(int PageIndex, int PageSize)
        {
            List<Demo> list = await _demoRepository.GetDemoList();
            int TotalCount = 1;
            TotalCount = list.Count() / PageSize;
            list = list.OrderBy(d => d.Age).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            Logger.Info(JsonConvert.SerializeObject(list)); //此处调用日志记录函数记录日志
            return Json(new
            {
                code = 0,
                message = "成功",
                page = PageIndex,
                pageCount = TotalCount,
                data = list
            });
        }

        /// <summary>
        /// 获取单个Demo
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet("GetDemo")]
        public async Task<JsonResult> GetDemo(Guid ID)
        {
            Demo model = await _demoRepository.GetDemo(ID);
            Logger.Info(JsonConvert.SerializeObject(model));//此处调用日志记录函数记录日志
            return Json(new
            {
                code = 0,
                message = true,
                data = model
            });
        }

        /// <summary>
        /// 新增Demo
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("AddDemo")]
        public async Task<JsonResult> AddDemo(Demo entity)
        {
            bool result = await _demoRepository.AddDemo(entity);
            Logger.Info(JsonConvert.SerializeObject(result));//此处调用日志记录函数记录日志
            return Json(new
            {
                code = 0,
                message = "成功",
                data = result
            });
        }

        /// <summary>
        /// 修改Demo
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("UpdateDemo")]
        public async Task<JsonResult> UpdateDemo(Demo entity)
        {
            bool result = await _demoRepository.UpdateDemo(entity);
            Logger.Info(JsonConvert.SerializeObject(result));//此处调用日志记录函数记录日志
            return Json(new
            {
                code = 0,
                message = "成功",
                data = result
            });
        }

        /// <summary>
        /// 删除Demo
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete("DeleteDemo")]
        public async Task<JsonResult> DeleteDemo(Guid ID)
        {
            bool result = await _demoRepository.DeleteDemo(ID);
            Logger.Info(JsonConvert.SerializeObject(result));//此处调用日志记录函数记录日志
            return Json(new
            {
                code = 0,
                message = "成功",
                data = result
            });
        }
    }
}