using Nzh.Admin.IRepository.Base;
using Nzh.Admin.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nzh.Admin.IRepository
{
    public interface IDemoRepository : IBaseRepository<Demo> 
    {
        /// <summary>
        /// 获取所有Demo
        /// </summary>
        /// <returns></returns>
        Task<List<Demo>> GetDemoList();

        /// <summary>
        /// 获取单个Demo
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<Demo> GetDemo(Guid ID);

        /// <summary>
        /// 新增Demo
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddDemo(Demo entity);

        /// <summary>
        /// 修改Demo
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateDemo(Demo entity);

        /// <summary>
        /// 删除Demo
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task DeleteDemo(Guid ID);
    }
}
