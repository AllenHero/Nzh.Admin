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
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : Controller
    {
        private readonly IDemoRepository demoRepository;
        public DemoController(IDemoRepository _demoRepository)
        {
            demoRepository = _demoRepository;
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public async Task<JsonResult> GetUsers()
        {
            List<Demo> list = await demoRepository.GetUsers();
            Logger.Info(JsonConvert.SerializeObject(list));//此处调用日志记录函数记录日志
            return Json(list);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task PostUser(Demo entity)
        {
            await demoRepository.PostUser(entity);
            Logger.Info(JsonConvert.SerializeObject(entity));//此处调用日志记录函数记录日志
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task PutUser(Demo entity)
        {
            try
            {
                await demoRepository.PutUser(entity);
                Logger.Info(JsonConvert.SerializeObject(entity));//此处调用日志记录函数记录日志
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task DeleteUser(Guid Id)
        {
            try
            {
                await demoRepository.DeleteUser(Id);
                Logger.Info(JsonConvert.SerializeObject(Id));//此处调用日志记录函数记录日志
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}