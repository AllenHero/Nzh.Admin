using Dapper;
using Nzh.Admin.Common.Base;
using Nzh.Admin.IRepository;
using Nzh.Admin.Model;
using Nzh.Admin.Model.Page;
using Nzh.Admin.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Nzh.Admin.Repository
{
    public class DemoRepository : BaseRepository<Demo>, IDemoRepository
    {
        IDbTransaction transaction = null;

        private readonly string DEFAULT_SORT_FIELD = "ID";

        /// <summary>
        /// 获取所有Demo
        /// </summary>
        /// <returns></returns>
        public async Task<PageResult<Demo>> GetDemoList(int PageIndex, int PageSize)
        {
            var result = new PageResult<Demo>();
            string sql = @"SELECT ID, Name, Sex, Age, Remark FROM [dbo].[Demo]";
            var DemoList= await GetList(sql);
            //判断page_size是否在0-100之间，超出范围则默认为20。
            PageSize = PageSize > 0 && PageSize <= 100 ? PageSize : 20;
            //判断page_size是否大于0，超出范围则默认为1。
            PageIndex = PageIndex > 0 ? PageIndex : 1;
            var maxPage = DemoList.Count == 0 ? DemoList.Count / PageSize : (DemoList.Count / PageSize) + 1;
            if (PageIndex > maxPage)
            {
                PageIndex = maxPage; //超过最大页数默认获取最后一页
            }
            result.page_num = PageIndex;
            result.page_size = PageSize;
            result.total = DemoList.Count;
            result.list = DemoList;
            return result;
        }

        /// <summary>
        /// 获取单个Demo
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<Demo> GetDemo(Guid ID)
        {
            string sql = @"SELECT ID, Name, Sex, Age, Remark FROM [dbo].[Demo] WHERE ID=@ID";
            return await Get(ID, sql);
        }

        /// <summary>
        /// 新增Demo
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddDemo(Demo entity)
        {
            try
            {
                using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
                {
                    transaction = conn.BeginTransaction();//开始事务
                    string sql = @"INSERT INTO [dbo].[Demo](ID, Name, Sex, Age, Remark) VALUES(@ID, @Name, @Sex, @Age, @Remark)";
                    await Add(entity, sql);
                    transaction.Commit();//提交事务
                } 
            }
            catch (Exception ex)
            {
                transaction.Rollback();//回滚
                throw ex;     
            }
        }

        /// <summary>
        /// 修改Demo
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateDemo(Demo entity)
        {
            try
            {
                using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
                {
                    transaction = conn.BeginTransaction();//开始事务
                    string sql = "UPDATE [dbo].[Demo] SET Name=@Name, Sex=@Sex, Age=@Age, Remark=@Remark WHERE ID=@ID";
                    await Update(entity, sql);
                    transaction.Commit(); //提交事务
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();//回滚
                throw ex;
            }
        }

        /// <summary>
        /// 删除Demo
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task DeleteDemo(Guid ID)
        {
            try
            {
                using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
                {
                    transaction = conn.BeginTransaction(); //开始事务
                    string sql = "DELETE FROM [dbo].[Demo] WHERE ID=@ID";
                    await DeleteByID(ID, sql);
                    transaction.Commit();//提交事务
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();//回滚
                throw ex;
            }
        }
    }
}
