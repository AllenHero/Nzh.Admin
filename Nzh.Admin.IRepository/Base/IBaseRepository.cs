using DapperExtensions;
using Nzh.Admin.Model.Base;
using Nzh.Admin.Model.Filter;
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

        IDbTransaction BeginTransaction();

        void CommitTransaction(IDbTransaction tran);

        void RollbackTransaction(IDbTransaction tran);

        Task<bool> ExecuteSqlAsync(string sql);

        bool ExecuteSql(string sql);

        Task<bool> InsertAsync(T entity, string sql);

        bool Insert(T entity, string sql);

        Task<bool> InsertRangeAsync(List<T> entitylist, string sql);

        bool InsertRange(List<T> entitylist, string sql);

        Task<bool> DeleteByIdAsync(object Id, string sql);

        bool DeleteById(object Id, string sql);

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

        Task<T> GetAsync(object Id, string sql);

        Task<T> GetAsync(string sql);

        T Get(object Id, string sql);

        T Get(string sql);

        Task<List<T>> GetListAsync(string sql);

        List<T> GetList(string sql);

        Task<List<T>> GetListAsync(string sql, int pageIndex, int pageSize);

        List<T> GetList(string sql, int pageIndex, int pageSize);

        Task<List<T>> GetListAsync(string sql, object param = null);

        List<T> GetList(string sql, object param = null);

        Task<List<T>> GetListAsync(string sql, int pageIndex, int pageSize, object param = null);

        List<T> GetList(string sql, int pageIndex, int pageSize, object param = null);

        #region  dapper扩展方法

        bool Insert(T model);

        Task<bool> InsertAsync(T model);

        bool Update(T model);

        Task<bool> UpdateAsync(T model);

        bool Delete(T model);

        Task<bool> DeleteAsync(T model);

        bool Delete(object predicate);

        Task<bool> DeleteAsync(object predicate);

        bool DeleteByWhere(string where, object param = null);

        Task<bool> DeleteByWhereAsync(string where, object param = null);

        T Get(object id);

        Task<T> GetAsync(object id);

        //T Get(object id, string keyName);

        List<T> GetList(object predicate = null, IList<ISort> sort = null);

        List<T> GetList(string where, string sort = null, int limits = -1, string fields = " * ", string orderby = "");

        int Count(object predicate = null);

        Task<int> CountAsync(object predicate = null);

        int CountByWhere(string where);

        Task<int> CountByWhereAsync(string where);

        List<T> GetPage(object predicate, IList<ISort> sort, int page, int resultsPerPage);

        PageDateRep<T> GetPage(string where, string sort, int page, int resultsPerPage, string fields = "*");

        #endregion 
    }
}
