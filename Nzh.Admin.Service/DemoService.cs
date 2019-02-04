using Nzh.Admin.Common.Base;
using Nzh.Admin.IRepository;
using Nzh.Admin.IService;
using Nzh.Admin.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Nzh.Admin.Service
{
    public class DemoService : IDemoService
    {
        private readonly IDemoRepository _demorepository;

        public DemoService(IDemoRepository demorepository)
        {
            _demorepository = demorepository;
        }

        IDbTransaction transaction = null;

        /// <summary>
        /// 获取所有Demo
        /// </summary>
        /// <returns></returns>
        public async Task<List<Demo>> GetDemoPageList()
        {
            string sql = @"SELECT ID, Name, Sex, Age, Remark FROM [dbo].[Demo]";
            return await _demorepository.GetList(sql);
        }

        /// <summary>
        /// 获取单个Demo
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<Demo> GetDemoById(Guid ID)
        {
            string sql = @"SELECT ID, Name, Sex, Age, Remark FROM [dbo].[Demo] WHERE ID=@ID";
            return await _demorepository.Get(ID, sql);
        }

        /// <summary>
        /// 新增Demo
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> AddDemo(Demo entity)
        {
            bool result = false;
            try
            {
                using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
                {
                    transaction = conn.BeginTransaction();//开始事务
                    string sql = @"INSERT INTO [dbo].[Demo](ID, Name, Sex, Age, Remark) VALUES(@ID, @Name, @Sex, @Age, @Remark)";
                    result = await _demorepository.Add(entity, sql);
                    transaction.Commit();//提交事务
                    result = true;
                    return result;
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
        public async Task<bool> UpdateDemo(Demo entity)
        {
            bool result = false;
            try
            {
                using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
                {
                    transaction = conn.BeginTransaction();//开始事务
                    string sql = "UPDATE [dbo].[Demo] SET Name=@Name, Sex=@Sex, Age=@Age, Remark=@Remark WHERE ID=@ID";
                    result = await _demorepository.Update(entity, sql);
                    transaction.Commit(); //提交事务
                    result = true;
                    return result;
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
        public async Task<bool> DeleteDemo(Guid ID)
        {
            bool result = false;
            try
            {
                using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
                {
                    transaction = conn.BeginTransaction(); //开始事务
                    string sql = "DELETE FROM [dbo].[Demo] WHERE ID=@ID";
                    result = await _demorepository.DeleteByID(ID, sql);
                    transaction.Commit();//提交事务
                    result = true;
                    return result;
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
