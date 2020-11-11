using Dapper;
using Nzh.Admin.IRepository.Base;
using Nzh.Admin.Model.Base;
using Nzh.Admin.Model.Filter;
using Nzh.Admin.Repository.Config;
using Nzh.Admin.Repository.SQLExts.MySQLExt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nzh.Admin.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        protected bool _RestoreMapping = true;

        /// <summary>
        /// 数据库连接信息
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            IDbConnection conn = DataBaseConfig.GetSqlConnection();
            return conn;
        }

        #region  新增

        /// <summary>
        /// 新增（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(T entity, string sql)
        {
            using (GetConnection())
            {
                return await GetConnection().ExecuteAsync(sql, entity)>0;
            }
        }

        /// <summary>
        ///  新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool Insert(T entity, string sql)
        {
            using (GetConnection())
            {
                return  GetConnection().Execute(sql, entity) > 0;
            }
        }

        /// <summary>
        /// 批量新增（异步）
        /// </summary>
        /// <param name="entitylist"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<bool> InsertRangeAsync(List<T> entitylist, string sql)
        {
            using (GetConnection())
            {
                return await GetConnection().ExecuteAsync(sql, entitylist)>0;
            }
        }

        /// <summary>
        ///  批量新增
        /// </summary>
        /// <param name="entitylist"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool InsertRange(List<T> entitylist, string sql)
        {
            using (GetConnection())
            {
                return  GetConnection().Execute(sql, entitylist) > 0;
            }
        }

        #endregion

        #region  删除

        /// <summary>
        /// 根据Id删除（异步）
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdAsync(long Id, string sql)
        {
            using (GetConnection())
            {
               return await GetConnection().ExecuteAsync(sql, new { Id = Id })>0;
            }
        }

        /// <summary>
        /// 根据Id删除
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool DeleteById(long Id, string sql)
        {
            using (GetConnection())
            {
                return  GetConnection().Execute(sql, new { Id = Id }) > 0;
            }
        }

        /// <summary>
        /// 删除（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(T entity, string sql)
        {
            using (GetConnection())
            {
               return await GetConnection().ExecuteAsync(sql, entity)>0;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool Delete(T entity, string sql)
        {
            using (GetConnection())
            {
                return  GetConnection().Execute(sql, entity) > 0;
            }
        }

        /// <summary>
        /// 批量删除（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRangeAsync(List<T> entitylist, string sql)
        {
            using (GetConnection())
            {
              return  await GetConnection().ExecuteAsync(sql, entitylist)>0;
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool DeleteRange(List<T> entitylist, string sql)
        {
            using (GetConnection())
            {
                return  GetConnection().Execute(sql, entitylist) > 0;
            }
        }

        #endregion

        #region 修改

        /// <summary>
        /// 修改（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(T entity, string sql)
        {
            using (GetConnection())
            {
               return await GetConnection().ExecuteAsync(sql, entity)>0;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool Update(T entity, string sql)
        {
            using (GetConnection())
            {
                return  GetConnection().Execute(sql, entity) > 0;
            }
        }

        /// <summary>
        /// 批量修改（异步）
        /// </summary>
        /// <param name="entitylist"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<bool> UpdateRangeAsync(List<T> entitylist, string sql)
        {
            using (GetConnection())
            {
               return await GetConnection().ExecuteAsync(sql, entitylist)>0;
            }
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="entitylist"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool UpdateRange(List<T> entitylist, string sql)
        {
            using (GetConnection())
            {
                return  GetConnection().Execute(sql, entitylist) > 0;
            }
        }

        #endregion

        #region   查询

        /// <summary>
        /// 返回数量（异步）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<int> CountAsync(string sql)
        {
            using (GetConnection())
            {
                return await GetConnection().ExecuteScalarAsync<int>(sql);
            }
        }

        /// <summary>
        /// 返回数量
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Count(string sql)
        {
            using (GetConnection())
            {
                return  GetConnection().ExecuteScalar<int>(sql);
            }
        }

        /// <summary>
        /// 获取实体（异步）
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(long Id, string sql)
        {
            using (GetConnection())
            {
                return await GetConnection().QueryFirstOrDefaultAsync<T>(sql, new { Id = Id });
            }
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public T Get(long Id, string sql)
        {
            using (GetConnection())
            {
                return  GetConnection().QueryFirstOrDefault<T>(sql, new { Id = Id });  
            }
        }

        /// <summary>
        /// 获取List（异步）
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync(string sql)
        {
            using (GetConnection())
            {
                return await Task.Run(() => GetConnection().Query<T>(sql).ToList());
            }
        }

        /// <summary>
        /// 获取List
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> GetList(string sql)
        {
            using (GetConnection())
            {
                return GetConnection().Query<T>(sql).ToList();
            }
        }

        /// <summary>
        /// 分页（异步）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync(string sql,int pageIndex, int pageSize)
        {
            using (GetConnection())
            {
                return await Task.Run(() => GetConnection().Query<T>(sql).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
            }
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<T> GetList(string sql, int pageIndex, int pageSize)
        {
            using (GetConnection())
            {
                return GetConnection().Query<T>(sql).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            }
        }

        /// <summary>
        /// 根据条件获取List（异步）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync(string sql , object param = null)
        {
            using (GetConnection())
            {
                return await Task.Run(() => GetConnection().Query<T>(sql, param).ToList());
            }
        }

        /// <summary>
        /// 根据条件获取List
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<T> GetList(string sql, object param = null)
        {
            using (GetConnection())
            {
                return  GetConnection().Query<T>(sql, param).ToList();
            }
        }

        /// <summary>
        /// 分页加条件（异步）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync(string sql, int pageIndex, int pageSize, object param = null )
        {
            using (GetConnection())
            {
                return await Task.Run(() => GetConnection().Query<T>(sql, param).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
            }
        }

        /// <summary>
        /// 分页加条件
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<T> GetList(string sql, int pageIndex, int pageSize, object param = null)
        {
            using (GetConnection())
            {
                return  GetConnection().Query<T>(sql, param).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            }
        }
        #endregion

        #region 扩展方法

        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public T Get(int Id)
        {
            using (GetConnection())
            {
                return GetConnection().GetById<T>(Id);
            }
        }

        /// <summary>
        /// 根据Id获取（异步）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(int Id)
        {
            using (GetConnection())
            {
                return await GetConnection().GetByIdAsync<T>(Id);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(T model)
        {
            using (GetConnection())
            {
                return GetConnection().Insert<T>(model);
            }
        }

        /// <summary>
        /// 新增（异步）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> InsertAsync(T model)
        {
            using (GetConnection())
            {
                return await GetConnection().InsertAsync<T>(model);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(T model)
        {
            using (GetConnection())
            {
                return GetConnection().UpdateById<T>(model);
            }
        }

        /// <summary>
        /// 更新（异步）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(T model)
        {
            using (GetConnection())
            {
                return await GetConnection().UpdateByIdAsync<T>(model);
            }
        }

        /// <summary>
        /// 更新列
        /// </summary>
        /// <param name="model"></param>
        /// <param name="updateFields"></param>
        /// <returns></returns>
        public int UpdateFields(T model, string updateFields)
        {
            using (GetConnection())
            {
                return GetConnection().UpdateById<T>(model, updateFields);
            }
        }

        /// <summary>
        /// 更新列（异步）
        /// </summary>
        /// <param name="model"></param>
        /// <param name="updateFields"></param>
        /// <returns></returns>
        public async Task<int> UpdateFieldsAsync(T model, string updateFields)
        {
            using (GetConnection())
            {
                return await GetConnection().UpdateByIdAsync<T>(model, updateFields);
            }
        }

        /// <summary>
        /// 根据Id删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            using (GetConnection())
            {
                return GetConnection().DeleteById<T>(Id);
            }
        }

        /// <summary>
        /// 根据Id删除（异步）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(int Id)
        {
            using (GetConnection())
            {
                return await GetConnection().DeleteByIdAsync<T>(Id);
            }
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int DeleteByWhere(string where)
        {
            using (GetConnection())
            {
                return GetConnection().DeleteByWhere<T>(where);
            }
        }

        /// <summary>
        /// 根据条件删除（异步）
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<int> DeleteByWhereAsync(string where)
        {
            using (GetConnection())
            {
                return await GetConnection().DeleteByWhereAsync<T>(where);
            }
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IEnumerable<T> GetByPage(SearchFilter filter, out long total)
        {
            using (GetConnection())
            {
                return GetConnection().GetByPage<T>(filter.pageIndex, filter.pageSize, out total, filter.returnFields, filter.where, filter.param, filter.orderBy, filter.transaction, filter.commandTimeout);
            }
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IEnumerable<T> GetByPageUnite(SearchFilter filter, out long total)
        {
            using (GetConnection())
            {
                return GetConnection().GetByPageUnite<T>(filter.pageIndex, filter.pageSize, out total, filter.returnFields, filter.where, filter.param, filter.orderBy, filter.transaction, filter.commandTimeout);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="returnFields"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAll(string returnFields = null, string orderby = null)
        {
            using (GetConnection())
            {
                return GetConnection().GetAll<T>(returnFields, orderby);
            }
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="where"></param>
        /// <param name="param"></param>
        /// <param name="returnFields"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public IEnumerable<T> GetByWhere(string where = null, object param = null, string returnFields = null, string orderby = null)
        {
            using (GetConnection())
            {
                return GetConnection().GetByWhere<T>(where, param, returnFields, orderby);
            }
        }

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public long GetTotal(SearchFilter filter)
        {
            using (GetConnection())
            {
                return GetConnection().GetTotal<T>(filter.where, filter.param);
            }
        }

        /// <summary>
        /// 查询总数（异步）
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<long> GetTotalAsync(SearchFilter filter)
        {
            using (GetConnection())
            {
                return await GetConnection().GetTotalAsync<T>(filter.where, filter.param);
            }
        }

        #endregion
    }
}
