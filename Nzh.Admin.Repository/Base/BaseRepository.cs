using Dapper;
using DapperExtensions;
using Nzh.Admin.IRepository.Base;
using Nzh.Admin.Model.Base;
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

        #region  添加

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<bool> Add(T entity, string sql)
        {
            using (GetConnection())
            {
                return await GetConnection().ExecuteAsync(sql, entity)>0;
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
            using (GetConnection())
            {
                return await GetConnection().ExecuteAsync(sql, entitylist)>0;
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
            using (GetConnection())
            {
               return await GetConnection().ExecuteAsync(sql, new { Id = Id })>0;
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
            using (GetConnection())
            {
               return await GetConnection().ExecuteAsync(sql, entity)>0;
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
            using (GetConnection())
            {
              return  await GetConnection().ExecuteAsync(sql, entitylist)>0;
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
            using (GetConnection())
            {
               return await GetConnection().ExecuteAsync(sql, entity)>0;
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
            using (GetConnection())
            {
               return await GetConnection().ExecuteAsync(sql, entitylist)>0;
            }
        }

        #endregion

        #region   查询

        /// <summary>
        /// 返回数量
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<int> Count(string sql)
        {
            using (GetConnection())
            {
                return await GetConnection().ExecuteScalarAsync<int>(sql);
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
            using (GetConnection())
            {
                return await GetConnection().QueryFirstOrDefaultAsync<T>(sql, new { Id = Id });
            }
        }

        /// <summary>
        /// 获取List
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<List<T>> GetList(string sql)
        {
            using (GetConnection())
            {
                return await Task.Run(() => GetConnection().Query<T>(sql).ToList());
            }
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<List<T>> GetList( string sql, int pageIndex, int pageSize)
        {
            int count = 0;
            using (GetConnection())
            {
                return await Task.Run(() => GetConnection().Query<T>(sql).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            using (GetConnection())
            {
                return await Task.Run(() => GetConnection().Query<T>(sql, param).ToList());
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
        public async Task<List<T>> GetList(string sql, int pageIndex, int pageSize, object param = null )
        {
            using (GetConnection())
            {
                return await Task.Run(() => GetConnection().Query<T>(sql, param).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
            }
        }
        #endregion

        #region Dapper扩展方法

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Insert(T model)
        {
            return _dapperExtension.Insert(model);
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool InsertBatch(List<T> models)
        {
            return _dapperExtension.InsertBatch(models);
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
        /// 批量更新
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool UpdateBatch(List<T> models)
        {
            return _dapperExtension.UpdateBatch(models);
        }


        /// <summary> 
        ///根据实体删除 id必须是int 或 guid
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(T model)
        {
            return _dapperExtension.Delete(model);
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
        /// 根据条件删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteByWhere(string where, object param = null)
        {
            return _dapperExtension.DeleteByWhere(where, param);
        }

        /// <summary>
        /// 根据实体删除 id必须是int 或 guid
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool DeleteBatch(List<T> models)
        {
            return _dapperExtension.DeleteBatch(models);
        }


        /// <summary>
        /// 获取一个实体对象
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public T Get(object id)
        {
            return _dapperExtension.Get(id);
        }

        /// <summary>
        /// 获取一个实体对象
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public T Get(object id, string keyName)
        {
            return _dapperExtension.Get(id, keyName);
        }


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
        /// 获取记录条数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int CountByWhere(string where)
        {
            return _dapperExtension.CountByWhere(where);
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

        /// <summary>
        /// Sql语句分页查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="resultsPerPage"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public  PageDateRep<T> GetPageList(string sql, string where, string sort, int page, int resultsPerPage, string fields = "*")
        {
            return  _dapperExtension.GetPageList(sql,where, sort, page, resultsPerPage, fields);
        }

        #endregion
    }
}
