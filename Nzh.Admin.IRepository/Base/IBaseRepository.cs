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

        Task<bool> AddAsync(T entity, string sql);

        bool Add(T entity, string sql);

        Task<bool> AddRangeAsync(List<T> entitylist, string sql);

        bool AddRange(List<T> entitylist, string sql);

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

        T Get(long Id, string sql);

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

        int Insert(T model);

        int Update(T model);

        int UpdateFields(T model, string updateFields);

        int Delete(int Id);

        int DeleteByWhere(string where);

        IEnumerable<T> GetByPage(SearchFilter filter, out long total);

        IEnumerable<T> GetByPageUnite(SearchFilter filter, out long total);

        IEnumerable<T> GetAll(string returnFields = null, string orderby = null);

        IEnumerable<T> GetByWhere(string where = null, object param = null, string returnFields = null, string orderby = null);

        long GetTotal(SearchFilter filter);

        #endregion
    }
}
