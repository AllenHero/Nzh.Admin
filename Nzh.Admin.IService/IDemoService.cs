using Nzh.Admin.IService.Base;
using Nzh.Admin.Model;
using Nzh.Admin.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nzh.Admin.IService
{
    public interface IDemoService : IBaseService
    {
        /// <summary>
        /// 获取Demo分页
        /// </summary>
        /// <returns></returns>
        Task<PageResult<Demo>> GetDemoPageListAsync(int PageIndex, int PageSize);

        /// <summary>
        /// 获取Demo
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<Demo> GetDemoByIdAsync(long Id);

        /// <summary>
        /// 添加Demo
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Sex"></param>
        /// <param name="Age"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        Task<OperationResult<bool>> InsertDemoAsync(string Name, string Sex, int Age, string Remark);

        /// <summary>
        /// 修改Demo
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Sex"></param>
        /// <param name="Age"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        Task<OperationResult<bool>> UpdateDemoAsync(long Id, string Name, string Sex, int Age, string Remark);

        /// <summary>
        /// 删除Demo
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<OperationResult<bool>> DeleteDemoAsync(long Id);
    }
}
