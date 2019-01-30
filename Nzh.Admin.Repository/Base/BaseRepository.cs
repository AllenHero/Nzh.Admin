using Dapper;
using Nzh.Admin.Common.Base;
using Nzh.Admin.IRepository.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nzh.Admin.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T>
    {
        protected bool _RestoreMapping = true;

        #region  添加

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<bool> Add(T entity, string sql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
                return await conn.ExecuteAsync(sql, entity)>0;
            }
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entitylist"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<bool> AddRange(List<T> entitylist, string sql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
                return await conn.ExecuteAsync(sql, entitylist)>0;
            }
        }

        #endregion

        #region  删除

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByID(Guid Id, string sql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
               return await conn.ExecuteAsync(sql, new { Id = Id })>0;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<bool> Delete(T entity, string sql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
               return await conn.ExecuteAsync(sql, entity)>0;
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRange(List<T> entitylist, string sql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
              return  await conn.ExecuteAsync(sql, entitylist)>0;
            }
        }

        #endregion

        #region 修改

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<bool> Update(T entity, string sql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
               return await conn.ExecuteAsync(sql, entity)>0;
            }
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="entitylist"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<bool> UpdateRange(List<T> entitylist, string sql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
               return await conn.ExecuteAsync(sql, entitylist)>0;
            }
        }

        #endregion


        /// <summary>
        /// 返回数量
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<int> Count(string sql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
                return await conn.ExecuteScalarAsync<int>(sql);
            }
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<T> Get(Guid Id, string sql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
                return await conn.QueryFirstOrDefaultAsync<T>(sql, new { Id = Id });
            }
        }

        /// <summary>
        /// 获取List
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<List<T>> GetList(string sql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
                return await Task.Run(() => conn.Query<T>(sql).ToList());
            }
        }

        /// <summary>
        /// 根据条件获取List
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<List<T>> GetList(string sql , object param = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
                return await Task.Run(() => conn.Query<T>(sql, param).ToList());
            }
        }
    }
}
