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

        Task<bool> DeleteByIdAsync(long Id, string sql);

        bool DeleteById(long Id, string sql);

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

        Task<T> GetAsync(long Id, string sql);

        Task<T> GetAsync(string sql);

        T Get(long Id, string sql);

        T Get(string sql);

        Task<List<T>> GetListAsync(string sql);

        List<T> GetList(string sql);

        Task<List<T>> GetListAsync(string sql, int pageIndex, int pageSize);

        List<T> GetList(string sql, int pageIndex, int pageSize);

        Task<List<T>> GetListAsync(string sql, object param = null);

        List<T> GetList(string sql, object param = null);

        Task<List<T>> GetListAsync(string sql, int pageIndex, int pageSize, object param = null);

        List<T> GetList(string sql, int pageIndex, int pageSize, object param = null);

        #region 扩展方法

        T Get(int Id);

        Task<T> GetAsync(int Id);

        int Insert(T model);

        Task<int> InsertAsync(T model);

        int Update(T model);

        Task<int> UpdateAsync(T model);

        int UpdateFields(T model, string updateFields);

        Task<int> UpdateFieldsAsync(T model, string updateFields);

        int Delete(int Id);

        Task<int> DeleteAsync(int Id);

        int DeleteByWhere(string where);

        Task<int> DeleteByWhereAsync(string where);

        IEnumerable<T> GetByPage(SearchFilter filter, out long total);

        IEnumerable<T> GetByPageUnite(SearchFilter filter, out long total);

        IEnumerable<T> GetAll(string returnFields = null, string orderby = null);

        IEnumerable<T> GetByWhere(string where = null, object param = null, string returnFields = null, string orderby = null);

        long GetTotal(SearchFilter filter);

        Task<long> GetTotalAsync(SearchFilter filter);

        #endregion
    }
}
