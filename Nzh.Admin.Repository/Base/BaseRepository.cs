using Dapper;
using Nzh.Admin.Common.Base;
using Nzh.Admin.IRepository.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nzh.Admin.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T>
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task Add(T entity, string sql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
                await conn.ExecuteAsync(sql, entity);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task Delete(Guid Id, string sql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
                await conn.ExecuteAsync(sql, new { Id = Id });
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task Update(T entity, string sql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
                await conn.ExecuteAsync(sql, entity);
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
        /// 返回数量
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<int> Count(string sql, object param = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
                return await conn.ExecuteScalarAsync<int>(sql, param);
            }
        }
    }
}
