using Dapper;
using DapperExtensions;
using Nzh.Admin.IRepository.Base;
using Nzh.Admin.Model.Base;
using Nzh.Admin.Model.Filter;
using Nzh.Admin.Repository.Config;
using Nzh.Admin.Repository.Extensions;
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

        DapperExtensions<T> _dapperExtension = new DapperExtensions<T>(); //dapper扩展

        /// <summary>
        /// 数据库连接信息
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            IDbConnection conn = DataBaseConfig.GetSqlConnection();
            return conn;
        }

        #region 事务

        /// <summary>
        /// 开始事务
        /// </summary>
        public IDbTransaction BeginTransaction()
        {
            IDbTransaction tran = GetConnection().BeginTransaction();
            return tran;
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTransaction(IDbTransaction tran)
        {
            tran.Commit();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="conn"></param>
        public void RollbackTransaction(IDbTransaction tran)
        {
            tran.Rollback();
        }

        #endregion

        #region Sql操作

        /// <summary>
        /// 执行sql（异步）
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<bool> ExecuteSqlAsync(string sql)
        {
            using (GetConnection())
            {
                return await GetConnection().ExecuteAsync(sql) > 0;
            }
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool ExecuteSql(string sql)
        {
            using (GetConnection())
            {
                return GetConnection().Execute(sql) > 0;
            }
        }

        #endregion

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
        public async Task<bool> DeleteByIdAsync(object Id, string sql)
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
        public bool DeleteById(object Id, string sql)
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
        public async Task<T> GetAsync(object Id, string sql)
        {
            using (GetConnection())
            {
                return await GetConnection().QueryFirstOrDefaultAsync<T>(sql, new { Id = Id });
            }
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(string sql)
        {
            using (GetConnection())
            {
                return await GetConnection().QueryFirstOrDefaultAsync<T>(sql);
            }
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public T Get(object Id, string sql)
        {
            using (GetConnection())
            {
                return  GetConnection().QueryFirstOrDefault<T>(sql, new { Id = Id });  
            }
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public T Get(string sql)
        {
            using (GetConnection())
            {
                return GetConnection().QueryFirstOrDefault<T>(sql);
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

        #region dapper扩展方法

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Insert(T model)
        {
            return _dapperExtension.Insert(model);
        }

        /// <summary>
        /// 新增（异步）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(T model)
        {
            return await _dapperExtension.InsertAsync(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(T model)
        {
            return _dapperExtension.Update(model);
        }

        /// <summary>
        /// 更新（异步）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(T model)
        {
            return await _dapperExtension.UpdateAsync(model);
        }

        /// <summary> 
        ///根据实体删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(T model)
        {
            return _dapperExtension.Delete(model);
        }

        /// <summary>
        /// 根据实体删除（异步）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(T model)
        {
            return await _dapperExtension.DeleteAsync(model);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(object predicate)
        {
            return _dapperExtension.Delete(predicate);
        }

        /// <summary>
        /// 根据条件删除（异步）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(object predicate)
        {
            return await _dapperExtension.DeleteAsync(predicate);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteByWhere(string where, object param = null)
        {
            return _dapperExtension.DeleteByWhere(where, param);
        }

        /// <summary>
        /// 根据条件删除（异步）
        /// </summary>
        /// <param name="where"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByWhereAsync(string where, object param = null)
        {
            return await _dapperExtension.DeleteByWhereAsync(where, param);
        }

        /// <summary>
        /// 根据id获取实体
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public T Get(object id)
        {
            return _dapperExtension.Get(id);
        }

        /// <summary>
        /// 根据id获取实体（异步）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(object id)
        {
            return await _dapperExtension.GetAsync(id);
        }

        /// <summary>
        /// 获取一个实体对象
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        //public T Get(object id, string keyName)
        //{
        //    return _dapperExtension.Get(id, keyName);
        //}

        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public List<T> GetList(object predicate = null, IList<ISort> sort = null)
        {
            return _dapperExtension.GetList(predicate, sort);
        }

        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="sort">排序</param>
        /// <param name="limits">前几条</param>
        /// <returns></returns>
        public List<T> GetList(string where, string sort = null, int limits = -1, string fields = " * ", string orderby = "")
        {
            return _dapperExtension.GetList(where, sort, limits, fields, orderby);
        }

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public int Count(object predicate = null)
        {
            return _dapperExtension.Count(predicate);
        }

        /// <summary>
        /// 获取记录条数（异步）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<int> CountAsync(object predicate = null)
        {
            return  await _dapperExtension.CountAsync(predicate);
        }

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int CountByWhere(string where)
        {
            return _dapperExtension.CountByWhere(where);
        }

        /// <summary>
        /// 获取记录条数（异步）
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<int> CountByWhereAsync(string where)
        {
            return await _dapperExtension.CountByWhereAsync(where);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="sort">排序</param>
        /// <param name="page">页索引</param>
        /// <param name="resultsPerPage">页大小</param>
        /// <returns></returns>
        public List<T> GetPage(object predicate, IList<ISort> sort, int page, int resultsPerPage)
        {
            page = page - 1;
            return _dapperExtension.GetPage(predicate, sort, page, resultsPerPage);
        }

        /// <summary>
        /// 存储过程分页查询
        /// </summary>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="resultsPerPage"></param>
        /// <returns></returns>
        public PageDateRep<T> GetPage(string where, string sort, int page, int resultsPerPage, string fields = "*")
        {
            return _dapperExtension.GetPage(where, sort, page, resultsPerPage, fields);
        }

        #endregion
    }
}
