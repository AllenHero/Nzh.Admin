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

        Task<bool> AddAsync(T entity, string sql);

        bool Add(T entity, string sql);

        Task<bool> AddRangeAsync(List<T> entitylist, string sql);

        bool AddRange(List<T> entitylist, string sql);

        Task<bool> DeleteByIDAsync(Guid Id, string sql);

        bool DeleteByID(Guid Id, string sql);

        Task<bool> DeleteAsync(T entity, string sql);

        bool Delete(T entity, string sql);

        Task<bool> DeleteRangeAsync(List<T> entitylist, string sql);

        bool DeleteRange(List<T> entitylist, string sql);

        Task<bool> UpdateAsync(T entity, string sql);

        bool Update(T entity, string sql);

        Task<bool> UpdateRangeAsync(List<T> entitylist, string sql);

        bool UpdateRange(List<T> entitylist, string sql);

        Task<int> CountAsync(string sql);

        int Count(string sql);

        Task<T> GetAsync(Guid Id, string sql);

        T Get(Guid Id, string sql);

        Task<List<T>> GetListAsync(string sql);

        List<T> GetList(string sql);

        Task<List<T>> GetListAsync(string sql, int pageIndex, int pageSize);

        List<T> GetList(string sql, int pageIndex, int pageSize);

        Task<List<T>> GetListAsync(string sql, object param = null);

        List<T> GetList(string sql, object param = null);

        Task<List<T>> GetListAsync(string sql, int pageIndex, int pageSize, object param = null);

        List<T> GetList(string sql, int pageIndex, int pageSize, object param = null);

        #region   Dapper扩展方法

        bool Insert(T model);

        Task<bool> InsertAsync(T model);

        bool InsertBatch(List<T> models);

        Task<bool> InsertBatchAsync(List<T> models);

        bool Update(T model);

        Task<bool> UpdateAsync(T model);

        bool UpdateBatch(List<T> models);

        Task<bool> UpdateBatchAsync(List<T> models);

        bool Delete(T model);

        Task<bool> DeleteAsync(T model);

        bool Delete(object predicate);

        Task<bool> DeleteAsync(object predicate);

        bool DeleteBatch(List<T> models);

        Task<bool> DeleteBatchAsync(List<T> models);

        bool DeleteByWhere(string where, object param = null);

        Task<bool> DeleteByWhereAsync(string where, object param = null);

        T Get(object id);

        Task<T> GetAsync(object id);

        T QueryFirst(string sql);

        Task<T> QueryFirstAsync(string sql);

        T QueryFirstOrDefault(string sql);

        Task<T> QueryFirstOrDefaultAsync(string sql);

        T QuerySingle(string sql);

        Task<T> QuerySingleAsync(string sql);

        T Query(object id, string keyName);

        Task<T> QueryAsync(object id, string keyName);

        List<T> GetList(object predicate = null, IList<ISort> sort = null);

        Task<List<T>> GetListAsync(object predicate = null, IList<ISort> sort = null);

        List<T> Query(string where, string sort = null, int limits = -1, string fileds = " * ", string orderby = "");

        Task<List<T>> QueryAsync(string where, string sort = null, int limits = -1, string fields = " * ", string orderby = "");

        int Count(object predicate = null);

        Task<int> CountAsync(object predicate = null);

        int CountByWhere(string where);

        Task<int> CountByWhereAsync(string where);

        List<T> GetPage(object predicate, IList<ISort> sort, int page, int resultsPerPage);

        Task<List<T>> GetPageAsync(object predicate, IList<ISort> sort, int page, int resultsPerPage);

        PageDateRep<T> GetPage(string where, string sort, int page, int resultsPerPage, string fields = "*");

        Task<PageDateRep<T>> GetPageAsync(string where, string sort, int page, int resultsPerPage, string fields = "*");

        #endregion
    }
}
