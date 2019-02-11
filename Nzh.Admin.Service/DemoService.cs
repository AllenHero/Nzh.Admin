﻿
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
        /// 获取Demo分页
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public async Task<List<Demo>> GetDemoPageList(int PageIndex, int PageSize)
        {
            string sql = @"SELECT ID, Name, Sex, Age, Remark FROM [dbo].[Demo]";
            List <Demo> demoList = await _demoRepository.GetList(sql, PageIndex, PageSize);
            return demoList;
        }

        /// <summary>
        /// 获取Demo
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<Demo> GetDemoById(Guid ID)
        {
            string sql = @"SELECT ID, Name, Sex, Age, Remark FROM [dbo].[Demo] WHERE ID=@ID";
            var model = await _demoRepository.Get(ID, sql);
            return model;
        }

        /// <summary>
        /// 添加Demo
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Sex"></param>
        /// <param name="Age"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        public async Task<OperationResult<bool>> AddDemo(string Name, string Sex, int Age, string Remark)
        {
            var result = new OperationResult<bool>();
            try
            {
                using (_demoRepository.GetConnection())
                {
                    Demo demo = new Demo();
                    demo.ID = Guid.NewGuid();
                    demo.Name = Name;
                    demo.Sex = Sex;
                    demo.Age = Age;
                    demo.Remark = Remark;
                    transaction = _demoRepository.GetConnection().BeginTransaction();//开始事务
                    string sql = @"INSERT INTO [dbo].[Demo](ID, Name, Sex, Age, Remark) VALUES(@ID, @Name, @Sex, @Age, @Remark)";
                    result.data = await _demoRepository.Add(demo, sql);
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
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        /// <param name="Sex"></param>
        /// <param name="Age"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        public async Task<OperationResult<bool>> UpdateDemo(Guid ID, string Name, string Sex, int Age, string Remark)
        {
            var result = new OperationResult<bool>();
            try
            {
                using (_demoRepository.GetConnection())
                {
                    Demo demo = new Demo();
                    demo.ID = ID;
                    demo.Name = Name;
                    demo.Sex = Sex;
                    demo.Age = Age;
                    demo.Remark = Remark;
                    transaction = _demoRepository.GetConnection().BeginTransaction();//开始事务
                    string sql = "UPDATE [dbo].[Demo] SET Name=@Name, Sex=@Sex, Age=@Age, Remark=@Remark WHERE ID=@ID";
                    result.data = await _demoRepository.Update(demo, sql);
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
