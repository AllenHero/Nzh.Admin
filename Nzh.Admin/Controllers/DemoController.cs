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
        private readonly IDemoRepository demoRepository;
        public DemoController(IDemoRepository _demoRepository)
        {
            demoRepository = _demoRepository;
        }

        /// <summary>
        /// 获取所有Demo
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> GetDemoList()
        {
            List<Demo> list = await demoRepository.GetDemoList();
            Logger.Info(JsonConvert.SerializeObject(list));//此处调用日志记录函数记录日志
            return Json(list);
        }

        /// <summary>
        /// 获取单个Demo
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> GetDemo(Guid ID)
        {
            Demo model = await demoRepository.GetDemo(ID);
            Logger.Info(JsonConvert.SerializeObject(model));//此处调用日志记录函数记录日志
            return Json(model);
        }

        /// <summary>
        /// 新增Demo
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddDemo(Demo entity)
        {
            await demoRepository.AddDemo(entity);
            Logger.Info(JsonConvert.SerializeObject(entity));//此处调用日志记录函数记录日志
        }

        /// <summary>
        /// 修改Demo
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task  UpdateDemo(Demo entity)
        {
            try
            {
                await demoRepository.UpdateDemo(entity);
                Logger.Info(JsonConvert.SerializeObject(entity));//此处调用日志记录函数记录日志
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// 删除Demo
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task DeleteDemo(Guid ID)
        {
            try
            {
                await demoRepository.DeleteDemo(ID);
                Logger.Info(JsonConvert.SerializeObject(ID));//此处调用日志记录函数记录日志
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}