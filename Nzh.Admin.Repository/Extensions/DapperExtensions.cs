using Dapper;
using DapperExtensions;
using Nzh.Admin.Model.Base;
using Nzh.Admin.Repository.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nzh.Admin.Repository.Extensions
{
    /// <summary>
    /// Dapper扩展
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class DapperExtensions<T> where T : class, new()
    {
        #region 插入

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Insert(T model)
        {
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                return cn.Insert(model);
            }
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(T model)
        {
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                return await cn.InsertAsync(model);
            }
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool InsertBatch(List<T> models)
        {
            bool result = false;
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                foreach (var model in models)
                {
                    cn.Insert(model);
                }
                result = true;
            }
            return result;
        }

        /// <summary>
        ///  批量插入
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public async Task<bool> InsertBatchAsync(List<T> models)
        {
            bool result = false;
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                foreach (var model in models)
                {
                    await cn.InsertAsync(model);
                }
                result = true;
            }
            return result;
        }

        #endregion

        #region 更新

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(T model)
        {
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                return cn.Update(model);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(T model)
        {
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                return await cn.UpdateAsync(model);
            }
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool UpdateBatch(List<T> models)
        {
            bool result = false;
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                foreach (var model in models)
                {
                    result = cn.Update(model);
                }
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public async Task<bool> UpdateBatchAsync(List<T> models)
        {
            bool result = false;
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                foreach (var model in models)
                {
                    result = await cn.UpdateAsync(model);
                }
                result = true;
            }
            return result;
        }

        #endregion

        #region 删除

        /// <summary>
        ///根据实体删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(T model)
        {
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                return cn.Delete(model);
            }
        }

        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(T model)
        {
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                return await cn.DeleteAsync(model);
            }
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(object predicate)
        {
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                return cn.Delete(predicate);
            }
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(object predicate)
        {
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                return await cn.DeleteAsync(predicate);
            }
        }

        /// <summary>
        /// 根据实体批量删除
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool DeleteBatch(List<T> models)
        {
            bool result = false;
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                foreach (var model in models)
                {
                    result = cn.Delete(model);
                }
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 根据实体批量删除
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public async Task<bool> DeleteBatchAsync(List<T> models)
        {
            bool result = false;
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                foreach (var model in models)
                {
                    result =await cn.DeleteAsync(model);
                }
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteByWhere(string where, object param = null)
        {
            bool result = false;
            var tableName = typeof(T).Name;
            StringBuilder sql = new StringBuilder().AppendFormat(" Delete FROM {0} ", tableName);
            if (string.IsNullOrEmpty(where))
            {
                return result;
            }
            sql.AppendFormat(" where {0} ", where);
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                result = cn.Execute(sql.ToString(), param) > 0;
                return result;
            }
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByWhereAsync(string where, object param = null)
        {
            bool result = false;
            var tableName = typeof(T).Name;
            StringBuilder sql = new StringBuilder().AppendFormat(" Delete FROM {0} ", tableName);
            if (string.IsNullOrEmpty(where))
            {
                return result;
            }
            sql.AppendFormat(" where {0} ", where);
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                result =await cn.ExecuteAsync(sql.ToString(), param) > 0;
                return result;
            }
        }

        #endregion

        #region   查询

        /// <summary>
        /// 根据一个实体对象
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public T Get(object id)
        {
            T t = default(T); //默认只对int guid主键有作用除非使用ClassMapper
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                t = cn.Get<T>(id);
            }
            return t;
        }

        /// <summary>
        /// 根据一个实体对象
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(object id)
        {
            T t = default(T); //默认只对int guid主键有作用除非使用ClassMapper
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                t =await cn.GetAsync<T>(id);
            }
            return t;
        }

        /// <summary>
        /// 获取一个实体对象
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public T Get(object id, string keyName)
        {
            var tableName = typeof(T).Name;
            StringBuilder sql = new StringBuilder().AppendFormat("SELECT  TOP 1 * FROM {0} WHERE {1}=@id ", tableName, keyName);
            var pms = new { id = id };
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                return cn.Query<T>(sql.ToString(), pms).FirstOrDefault();
            }
        }

        /// <summary>
        /// 获取一个实体对象
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(object id, string keyName)
        {
            var tableName = typeof(T).Name;
            StringBuilder sql = new StringBuilder().AppendFormat("SELECT  TOP 1 * FROM {0} WHERE {1}=@id ", tableName, keyName);
            var pms = new { id = id };
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                return await Task.Run(() => cn.Query<T>(sql.ToString(), pms).FirstOrDefault());
            }
        }      
        
        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public List<T> GetList(object predicate = null, IList<ISort> sort = null)
        {
            List<T> t = null;
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                t = cn.GetList<T>(predicate, sort).ToList();//不使用ToList  SqlConnection未初始化
            }
            return t;
        }

        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync(object predicate = null, IList<ISort> sort = null)
        {
            List<T> t = null;
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                t = await Task.Run(() => cn.GetList<T>(predicate, sort).ToList());//不使用ToList  SqlConnection未初始化
            }
            return t;
        }


        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public List<T> GetList(string where, string sort = null, int limits = -1, string fileds = " * ", string orderby = "")
        {
            var tableName = typeof(T).Name;
            StringBuilder sql = new StringBuilder().AppendFormat("SELECT " + (limits > 0 ? (" TOP " + limits) : " ") + fileds + "  FROM {0} {1} ",
                tableName, (string.IsNullOrWhiteSpace(orderby) ? "" : (" order by " + orderby)));
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where {0} ", where);
            }
            if (!string.IsNullOrEmpty(sort))
            {
                sql.AppendFormat(" order by {0} ", sort);
            }
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                return cn.Query<T>(sql.ToString()).ToList();
            }
        }

        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync(string where, string sort = null, int limits = -1, string fileds = " * ", string orderby = "")
        {
            var tableName = typeof(T).Name;
            StringBuilder sql = new StringBuilder().AppendFormat("SELECT " + (limits > 0 ? (" TOP " + limits) : " ") + fileds + "  FROM {0} {1} ",
                tableName, (string.IsNullOrWhiteSpace(orderby) ? "" : (" order by " + orderby)));
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where {0} ", where);
            }
            if (!string.IsNullOrEmpty(sort))
            {
                sql.AppendFormat(" order by {0} ", sort);
            }
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                return await Task.Run(() => cn.Query<T>(sql.ToString()).ToList());
            }
        }

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public int Count(object predicate = null)
        {
            int t = 0;
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                t = cn.Count<T>(predicate);
            }
            return t;
        }

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public async Task<int> CountAsync(object predicate = null)
        {
            int t = 0;
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                t = await cn.CountAsync<T>(predicate);
            }
            return t;
        }

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public int CountByWhere(string where)
        {
            var tableName = typeof(T).Name;
            StringBuilder sql = new StringBuilder().AppendFormat("SELECT COUNT(1) FROM {0} ", tableName);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where {0} ", where);
            }
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                return cn.ExecuteScalar<int>(sql.ToString());
            }
        }

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public async Task<int> CountByWhereAsync(string where)
        {
            var tableName = typeof(T).Name;
            StringBuilder sql = new StringBuilder().AppendFormat("SELECT COUNT(1) FROM {0} ", tableName);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where {0} ", where);
            }
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                return await cn.ExecuteScalarAsync<int>(sql.ToString());
            }
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
            List<T> t = null;
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                t = cn.GetPage<T>(predicate, sort, page, resultsPerPage).ToList();
            }
            return t;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="sort">排序</param>
        /// <param name="page">页索引</param>
        /// <param name="resultsPerPage">页大小</param>
        /// <returns></returns>
        public async Task<List<T>> GetPageAsync(object predicate, IList<ISort> sort, int page, int resultsPerPage)
        {
            List<T> t = null;
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                t = await Task.Run(() => cn.GetPage<T>(predicate, sort, page, resultsPerPage).ToList());
            }
            return t;
        }

        /// <summary>
        /// 存储过程分页查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="sort">分类</param>
        /// <param name="page">页索引</param>
        /// <param name="resultsPerPage">页大小</param>
        /// <param name="fields">查询字段</param>
        /// <returns></returns>
        public PageDateRep<T> GetPage(string where, string sort, int page, int resultsPerPage, string fields = "*", Type result = null)
        {
            var tableName = typeof(T).Name;
            var p = new DynamicParameters();
            p.Add("@TableName", tableName);
            p.Add("@Fields", fields);
            p.Add("@OrderField", sort);
            p.Add("@sqlWhere", where);
            p.Add("@pageSize", resultsPerPage);
            p.Add("@pageIndex", page);
            p.Add("@TotalPage", 0, direction: ParameterDirection.Output);
            p.Add("@Totalrow", 0, direction: ParameterDirection.Output);
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                var data = cn.Query<T>("P_ZGrid_PagingLarge", p, commandType: CommandType.StoredProcedure, commandTimeout: 120);
                int totalPage = p.Get<int>("@TotalPage");
                int totalrow = p.Get<int>("@Totalrow");
                var rep = new PageDateRep<T>()
                {
                    code = 0,
                    count = totalrow,
                    totalPage = totalPage,
                    data = data.ToList(),
                    PageNum = page,
                    PageSize = resultsPerPage
                };
                return rep;
            }
        }

        /// <summary>
        /// 存储过程分页查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="sort">分类</param>
        /// <param name="page">页索引</param>
        /// <param name="resultsPerPage">页大小</param>
        /// <param name="fields">查询字段</param>
        /// <returns></returns>
        public async Task<PageDateRep<T>> GetPageAsync(string where, string sort, int page, int resultsPerPage, string fields = "*", Type result = null)
        {
            var tableName = typeof(T).Name;
            var p = new DynamicParameters();
            p.Add("@TableName", tableName);
            p.Add("@Fields", fields);
            p.Add("@OrderField", sort);
            p.Add("@sqlWhere", where);
            p.Add("@pageSize", resultsPerPage);
            p.Add("@pageIndex", page);
            p.Add("@TotalPage", 0, direction: ParameterDirection.Output);
            p.Add("@Totalrow", 0, direction: ParameterDirection.Output);
            using (SqlConnection cn = new SqlConnection(DataBaseConfig.ConnStr))
            {
                var data = await Task.Run(() => cn.Query<T>("P_ZGrid_PagingLarge", p, commandType: CommandType.StoredProcedure, commandTimeout: 120));
                int totalPage = p.Get<int>("@TotalPage");
                int totalrow = p.Get<int>("@Totalrow");
                var rep = new PageDateRep<T>()
                {
                    code = 0,
                    count = totalrow,
                    totalPage = totalPage,
                    data = data.ToList(),
                    PageNum = page,
                    PageSize = resultsPerPage
                };
                return rep;
            }
        }
        #endregion
    }
}
