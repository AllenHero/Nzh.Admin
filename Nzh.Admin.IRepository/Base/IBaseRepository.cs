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

        Task<bool> Add(T entity, string sql);

        Task<bool> AddRange(List<T> entitylist, string sql);

        Task<bool> DeleteByID(Guid Id, string sql);

        Task<bool> Delete(T entity, string sql);

        Task<bool> DeleteRange(List<T> entitylist, string sql);

        Task<bool> Update(T entity, string sql);

        Task<bool> UpdateRange(List<T> entitylist, string sql);

        Task<int> Count(string sql);

        Task<T> Get(Guid Id, string sql);

        Task<List<T>> GetList(string sql);

        Task<List<T>> GetList(string sql, int pageIndex, int pageSize);

        Task<List<T>> GetList(string sql, object param = null);

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

        PageDateRep<T> GetPageList(string sql, string where, string sort, int page, int resultsPerPage, string fields = "*");

    }
}
