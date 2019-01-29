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
        public async Task Delete(Guid Id, string sql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
                await conn.ExecuteAsync(sql, new { Id = Id });
            }
        }

        public async Task<T> Get(Guid Id, string sql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
                return await conn.QueryFirstOrDefaultAsync<T>(sql, new { Id = Id });
            }
        }

        public async Task<List<T>> ExecuteSP(string SpName)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
                return await Task.Run(() => conn.Query<T>(SpName, null, null, true, null, CommandType.StoredProcedure).ToList());
            }
        }

        public async Task Add(T entity, string sql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
                await conn.ExecuteAsync(sql, entity);
            }
        }

        public async Task<List<T>> GetList(string sql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
                return await Task.Run(() => conn.Query<T>(sql).ToList());
            }
        }

        public async Task Update(T entity, string sql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
                await conn.ExecuteAsync(sql, entity);
            }
        }
    }
}
