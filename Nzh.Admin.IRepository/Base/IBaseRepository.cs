using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nzh.Admin.IRepository.Base
{
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sql"></param>
        Task Add(T entity, string sql);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task Delete(Guid Id, string sql);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sql"></param>
        Task Update(T entity, string sql);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<T> Get(Guid Id, string sql);

        /// <summary>
        /// 获取List
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<List<T>> GetList(string sql);

        /// <summary>
        /// 返回数量
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<int> Count(string sql, object param = null);

    }
}
