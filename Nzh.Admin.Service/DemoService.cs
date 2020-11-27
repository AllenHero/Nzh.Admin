
using Nzh.Admin.IRepository;
using Nzh.Admin.IService;
using Nzh.Admin.Model;
using Nzh.Admin.Model.Base;
using Nzh.Admin.Service.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Nzh.Admin.Service
{
    public class DemoService : BaseService, IDemoService
    {
        private readonly IDemoRepository _demoRepository;

        public DemoService(IDemoRepository demoRepository)
        {
            _demoRepository = demoRepository;
        }

        /// <summary>
        /// 获取Demo分页
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public async Task<PageResult<Demo>> GetDemoPageListAsync(int PageIndex, int PageSize)
        {
            var result = new PageResult<Demo>();
            string sql = @"SELECT Id, Name, Sex, Age, Remark FROM Demo";
            string sqlCount = @"SELECT count(*) FROM Demo";
            result.list = await _demoRepository.GetListAsync(sql, PageIndex, PageSize);
            result.TotalCount = await _demoRepository.CountAsync(sqlCount);
            result.PageIndex = PageIndex;
            result.PageSize = PageSize;
            return result;
        }

        /// <summary>
        /// 获取Demo
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Demo> GetDemoByIdAsync(long Id)
        {
            string sql = @"SELECT Id, Name, Sex, Age, Remark FROM Demo WHERE Id=@Id";
            var demoModel = await _demoRepository.GetAsync(Id, sql);
            //var demoModel = await _demoRepository.GetAsync(Id);//dapper扩展方法
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
        public async Task<OperationResult<bool>> InsertDemoAsync(string Name, string Sex, int Age, string Remark)
        {
            using (IDbTransaction tran = _demoRepository.BeginTransaction())//开始事务
            {
                var result = new OperationResult<bool>();
                try
                {
                    byte[] buffer = Guid.NewGuid().ToByteArray();
                    Demo demo = new Demo();
                    demo.Id = BitConverter.ToInt64(buffer, 0);
                    demo.Name = Name;
                    demo.Sex = Sex;
                    demo.Age = Age;
                    demo.Remark = Remark;
                    //string sql = @"INSERT INTO Demo(Id, Name, Sex, Age, Remark) VALUES(@Id, @Name, @Sex, @Age, @Remark)";
                    //StringBuilder sb = new StringBuilder();
                    //sb.AppendFormat(" INSERT INTO `Demo` (`Id`, `Name`, `Sex`, `Age`, `Remark`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');", GuidToLongId(), Name, Sex, Age, Remark);
                    //result.data = await _demoRepository.InsertAsync(demo,sql);//dapper
                    //result.data = await _demoRepository.ExecuteSqlAsync(sb.ToString());//执行sql
                    result.data = await _demoRepository.InsertAsync(demo);//dapper扩展方法
                    _demoRepository.CommitTransaction(tran);//提交事务
                    return result;
                }
                catch (Exception ex)
                {
                    _demoRepository.RollbackTransaction(tran);//回滚事务
                    throw ex;
                }
            }
        }

        public static long GuidToLongId()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
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
        public async Task<OperationResult<bool>> UpdateDemoAsync(long Id, string Name, string Sex, int Age, string Remark)
        {
            using (IDbTransaction tran = _demoRepository.BeginTransaction())//开始事务
            {
                var result = new OperationResult<bool>();
                try
                {
                    Demo demo = new Demo();
                    demo.Id = Id;
                    demo.Name = Name;
                    demo.Sex = Sex;
                    demo.Age = Age;
                    demo.Remark = Remark;
                    string sql = "UPDATE Demo SET Name=@Name, Sex=@Sex, Age=@Age, Remark=@Remark WHERE Id=@Id";
                    result.data = await _demoRepository.UpdateAsync(demo, sql);
                    //result.data = await _demoRepository.UpdateAsync(demo);//dapper扩展方法
                    _demoRepository.CommitTransaction(tran);//提交事务
                    return result;
                }
                catch (Exception ex)
                {
                    _demoRepository.RollbackTransaction(tran);//回滚事务
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 删除Demo
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<OperationResult<bool>> DeleteDemoAsync(long Id)
        {
            using (IDbTransaction tran = _demoRepository.BeginTransaction()) //开始事务
            {
                var result = new OperationResult<bool>();
                try
                {
                    string sql = "DELETE FROM Demo WHERE Id=@Id";
                    result.data = await _demoRepository.DeleteByIdAsync(Id, sql);
                    //result.data = await _demoRepository.DeleteAsync(Id);//dapper扩展方法
                    _demoRepository.CommitTransaction(tran);//提交事务
                    return result;
                }
                catch (Exception ex)
                {
                    _demoRepository.RollbackTransaction(tran);//回滚事务
                    throw ex;
                }
            }
        }
    }
}
