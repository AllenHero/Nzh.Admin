using Dapper;
using Nzh.Admin.IRepository.Base;
using Nzh.Admin.Model.Base;
using Nzh.Admin.Model.Filter;
using Nzh.Admin.Repository.Config;
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
        public T Get(long Id, string sql)
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
    }
}
