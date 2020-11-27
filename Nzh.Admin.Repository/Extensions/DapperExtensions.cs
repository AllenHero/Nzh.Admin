using Dapper;
using DapperExtensions;
using Nzh.Admin.Model.Base;
using Nzh.Admin.Repository.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Nzh.Admin.Repository.Extensions
{
    /// <summary>
    /// Dapper扩展
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class DapperExtensions<T> where T : class, new()
    {
        /// <summary>
        /// 数据库连接信息
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            IDbConnection conn = DataBaseConfig.GetSqlConnection();
            return conn;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Insert(T model)
        {
            using (GetConnection())
            {
                return GetConnection().Insert(model);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(T model)
        {
            using (GetConnection())
            {
                return GetConnection().Update(model);
            }
        }

        /// <summary>
        ///根据实体删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(T model)
        {
            using (GetConnection())
            {
                return GetConnection().Delete(model);
            }
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(object predicate)
        {
            using (GetConnection())
            {
                return GetConnection().Delete(predicate);
            }
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteByWhere(string where, object param = null)
        {
            var tableName = typeof(T).Name;
            StringBuilder sql = new StringBuilder().AppendFormat(" Delete FROM {0} ", tableName);
            sql.AppendFormat(" where {0} ", where);
            using (GetConnection())
            {
                return GetConnection().Execute(sql.ToString(), param) > 0;
            }
        }

        /// <summary>
        /// 根据一个实体对象
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public T Get(object id)
        {
            using (GetConnection())
            {
                return GetConnection().Get<T>(id);
            }
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
            using (GetConnection())
            {
                return GetConnection().Query<T>(sql.ToString(), pms).FirstOrDefault();
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
            using (GetConnection())
            {
                return GetConnection().GetList<T>(predicate, sort).ToList();//不使用ToList  SqlConnection未初始化
            }
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
            using (GetConnection())
            {
                return GetConnection().Query<T>(sql.ToString()).ToList();
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
            using (GetConnection())
            {
                return GetConnection().Count<T>(predicate);
            }
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
            using (GetConnection())
            {
                return GetConnection().ExecuteScalar<int>(sql.ToString());
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
            using (GetConnection())
            {
                return GetConnection().GetPage<T>(predicate, sort, page, resultsPerPage).ToList();
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
            using (GetConnection())
            {
                var data = GetConnection().Query<T>("P_ZGrid_PagingLarge", p, commandType: CommandType.StoredProcedure, commandTimeout: 120);
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
    }
}
