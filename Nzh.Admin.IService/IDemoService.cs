using Nzh.Admin.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nzh.Admin.IService
{
    public interface IDemoService
    {
        /// <summary>
        /// 获取所有Demo
        /// </summary>
        /// <returns></returns>
        Task<List<Demo>> GetDemoPageList();

        /// <summary>
        /// 获取单个Demo
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<Demo> GetDemoById(Guid ID);

        /// <summary>
        /// 新增Demo
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> AddDemo(Demo entity);

        /// <summary>
        /// 修改Demo
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateDemo(Demo entity);

        /// <summary>
        /// 删除Demo
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<bool> DeleteDemo(Guid ID);
    }
}
