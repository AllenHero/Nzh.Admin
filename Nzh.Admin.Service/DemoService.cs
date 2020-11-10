
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
        public async Task<PageResult<Demo>> GetDemoPageList(int PageIndex, int PageSize)
        {
            var demoList = new PageResult<Demo>();
            string sql = @"SELECT Id, Name, Sex, Age, Remark FROM Demo";
            string sqlCount = @"SELECT count(*) FROM Demo";
            demoList.list = await _demoRepository.GetListAsync(sql, PageIndex, PageSize);
            demoList.TotalCount = await _demoRepository.CountAsync(sqlCount);
            demoList.PageIndex = PageIndex;
            demoList.PageSize = PageSize;
            return demoList;
        }

        /// <summary>
        /// 获取Demo
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Demo> GetDemoById(long Id)
        {
            string sql = @"SELECT Id, Name, Sex, Age, Remark FROM Demo WHERE Id=@Id";
            var demoModel = await _demoRepository.GetAsync(Id, sql);
            return demoModel;
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
                    byte[] buffer = Guid.NewGuid().ToByteArray();
                    Demo demo = new Demo();
                    demo.Id = BitConverter.ToInt64(buffer, 0);
                    demo.Name = Name;
                    demo.Sex = Sex;
                    demo.Age = Age;
                    demo.Remark = Remark;
                    transaction = _demoRepository.GetConnection().BeginTransaction();//开始事务
                    string sql = @"INSERT INTO Demo(Id, Name, Sex, Age, Remark) VALUES(@Id, @Name, @Sex, @Age, @Remark)";
                    result.data = await _demoRepository.AddAsync(demo, sql);
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
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Sex"></param>
        /// <param name="Age"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        public async Task<OperationResult<bool>> UpdateDemo(long Id, string Name, string Sex, int Age, string Remark)
        {
            var result = new OperationResult<bool>();
            try
            {
                using (_demoRepository.GetConnection())
                {
                    Demo demo = new Demo();
                    demo.Id = Id;
                    demo.Name = Name;
                    demo.Sex = Sex;
                    demo.Age = Age;
                    demo.Remark = Remark;
                    transaction = _demoRepository.GetConnection().BeginTransaction();//开始事务
                    string sql = "UPDATE Demo SET Name=@Name, Sex=@Sex, Age=@Age, Remark=@Remark WHERE Id=@Id";
                    result.data = await _demoRepository.UpdateAsync(demo, sql);
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
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<OperationResult<bool>> DeleteDemo(long Id)
        {
            var result = new OperationResult<bool>();
            try
            {
                using (_demoRepository.GetConnection())
                {
                    transaction = _demoRepository.GetConnection().BeginTransaction(); //开始事务
                    string sql = "DELETE FROM Demo WHERE Id=@Id";
                    result.data = await _demoRepository.DeleteByIdAsync(Id, sql);
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
