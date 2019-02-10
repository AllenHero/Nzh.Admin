using DapperExtensions;
using Nzh.Admin.Model.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Nzh.Admin.IRepository.Base
{
    public interface IBaseRepository<T> where T : class, new()
    {
        IDbConnection GetConnection();


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sql"></param>
        Task<bool> Add(T entity, string sql);

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entitylist"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<bool> AddRange(List<T> entitylist, string sql);

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<bool> DeleteByID(Guid Id, string sql);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<bool> Delete(T entity, string sql);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<bool> DeleteRange(List<T> entitylist, string sql);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sql"></param>
        Task<bool> Update(T entity, string sql);

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="entitylist"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<bool> UpdateRange(List<T> entitylist, string sql);

        /// <summary>
        /// 返回数量
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<int> Count(string sql);

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
        /// 分页
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<T>> GetList(string sql, int pageIndex, int pageSize);

        /// <summary>
        /// 根据条件获取List
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<List<T>> GetList(string sql, object param = null);

        /// <summary>
        /// 分页加条件
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<List<T>> GetList(string sql, int pageIndex, int pageSize, object param = null);

        bool Insert(T model);

        bool InsertBatch(List<T> models);

        bool Update(T model);

        bool UpdateBatch(List<T> models);

        bool Delete(T model);

        bool Delete(object predicate);

        bool DeleteByWhere(string where, object param = null);

        bool DeleteBatch(List<T> models);

        T Get(object id);

        T Get(object id, string keyName);

        List<T> GetList(object predicate = null, IList<ISort> sort = null);

        List<T> GetList(string where, string sort = null, int limits = -1, string fileds = " * ", string orderby = "");

        int Count(object predicate = null);

        int CountByWhere(string where);

        List<T> GetPage(object predicate, IList<ISort> sort, int page, int resultsPerPage);

        PageDateRep<T> GetPage(string where, string sort, int page, int resultsPerPage, string fields = "*");

    }
}
