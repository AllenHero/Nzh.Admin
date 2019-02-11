
using Nzh.Admin.IRepository;
using Nzh.Admin.IService;
using Nzh.Admin.Model;
using Nzh.Admin.Model.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Nzh.Admin.Service
{
    public class DemoService : IDemoService
    {
        private readonly IDemoRepository _demoRepository;

        public DemoService(IDemoRepository demoRepository)
        {
            _demoRepository = demoRepository;
        }

        IDbTransaction transaction = null;

        /// <summary>
        /// 获取所有Demo
        /// </summary>
        /// <returns></returns>
        public async Task<List<Demo>> GetDemoPageList(int PageIndex, int PageSize)
        {
            string sql = @"SELECT ID, Name, Sex, Age, Remark FROM [dbo].[Demo]";
            var result = await _demoRepository.GetList(sql, PageIndex, PageSize);
            return result;
        }

        /// <summary>
        /// 获取单个Demo
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<Demo> GetDemoById(Guid ID)
        {
            string sql = @"SELECT ID, Name, Sex, Age, Remark FROM [dbo].[Demo] WHERE ID=@ID";
            return await _demoRepository.Get(ID, sql);
        }

        /// <summary>
        /// 新增Demo
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<OperationResult<bool>> AddDemo(Demo entity)
        {
            var result = new OperationResult<bool>();
            try
            {
                using (_demoRepository.GetConnection())
                {
                    transaction = _demoRepository.GetConnection().BeginTransaction();//开始事务
                    string sql = @"INSERT INTO [dbo].[Demo](ID, Name, Sex, Age, Remark) VALUES(@ID, @Name, @Sex, @Age, @Remark)";
                    result.data = await _demoRepository.Add(entity, sql);
                    //result = _demoRepository.Insert(entity);  //dapper扩展方法
                    transaction.Commit();//提交事务
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
        public async Task<OperationResult<bool>> UpdateDemo(Demo entity)
        {
            var result = new OperationResult<bool>();
            try
            {
                using (_demoRepository.GetConnection())
                {
                    transaction = _demoRepository.GetConnection().BeginTransaction();//开始事务
                    string sql = "UPDATE [dbo].[Demo] SET Name=@Name, Sex=@Sex, Age=@Age, Remark=@Remark WHERE ID=@ID";
                    result.data = await _demoRepository.Update(entity, sql);
                    transaction.Commit(); //提交事务
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
        public async Task<OperationResult<bool>> DeleteDemo(Guid ID)
        {
            var result = new OperationResult<bool>();
            try
            {
                using (_demoRepository.GetConnection())
                {
                    transaction = _demoRepository.GetConnection().BeginTransaction(); //开始事务
                    string sql = "DELETE FROM [dbo].[Demo] WHERE ID=@ID";
                    result.data = await _demoRepository.DeleteByID(ID, sql);
                    transaction.Commit();//提交事务
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
