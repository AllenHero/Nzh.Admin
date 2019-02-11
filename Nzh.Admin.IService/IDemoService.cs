using Nzh.Admin.Model;
using Nzh.Admin.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nzh.Admin.IService
{
    public interface IDemoService
    {
        /// <summary>
        /// 获取Demo分页
        /// </summary>
        /// <returns></returns>
        Task<List<Demo>> GetDemoPageList(int PageIndex, int PageSize);

        /// <summary>
        /// 获取Demo
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<Demo> GetDemoById(Guid ID);

        /// <summary>
        /// 添加Demo
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Sex"></param>
        /// <param name="Age"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        Task<OperationResult<bool>> AddDemo(string Name, string Sex, int Age, string Remark);

        /// <summary>
        /// 修改Demo
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        /// <param name="Sex"></param>
        /// <param name="Age"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        Task<OperationResult<bool>> UpdateDemo(Guid ID, string Name, string Sex, int Age, string Remark);

        /// <summary>
        /// 删除Demo
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<OperationResult<bool>> DeleteDemo(Guid ID);
    }
}
